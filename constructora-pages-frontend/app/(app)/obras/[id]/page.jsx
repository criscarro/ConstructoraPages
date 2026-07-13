"use client";

import { useState, useEffect } from "react";
import { useParams, useRouter } from "next/navigation";
import {
  ObtenerObraPorId,
  ObtenerEtapasObra,
  ObtenerAsignacionesEmpleado,
  ObtenerDetallesCompra,
  ObtenerEmpleados,
  AltaAsignacionEmpleado,
} from "../../../Api/api";

const nombresEstado = [
  "Pendiente",
  "En planificación",
  "En ejecución",
  "Pausa",
  "Finalizada",
  "Cancelada",
];

const nombresTipoObra = ["Residencial", "Industrial", "Civil", "Comercial"];

const nombresIntervencion = [
  "Obra nueva",
  "Reforma",
  "Rehabilitación",
  "Demolición",
];

export default function DetalleObra() {
  const { id } = useParams();
  const router = useRouter();

  const [obra, setObra] = useState(null);
  const [etapas, setEtapas] = useState([]);
  const [asignaciones, setAsignaciones] = useState([]);
  const [detallesCompra, setDetallesCompra] = useState([]);
  const [cargando, setCargando] = useState(true);
  const [pestanaActiva, setPestanaActiva] = useState("info");
  const [empleados, setEmpleados] = useState([]);
  const [mostrarFormAsignacion, setMostrarFormAsignacion] = useState(false);
  const [empleadoSeleccionado, setEmpleadoSeleccionado] = useState("");
  const [fechaAsignacion, setFechaAsignacion] = useState("");
  const [errorAsignacion, setErrorAsignacion] = useState("");

  useEffect(() => {
    const cargarDatos = async () => {
      setCargando(true);
      const obraId = Number(id);

      try {
        const [datosObra, datosEtapas, datosAsig, datosDetalles, datosEmpleados] = await Promise.all([
          ObtenerObraPorId(obraId),
          ObtenerEtapasObra(),
          ObtenerAsignacionesEmpleado(),
          ObtenerDetallesCompra(),
          ObtenerEmpleados(),
        ]);

        setObra(datosObra);
        setEtapas(datosEtapas.filter((e) => e.obra?.id === obraId));
        setAsignaciones(datosAsig.filter((a) => a.obra?.id === obraId));
        setDetallesCompra(
          datosDetalles.filter((d) => d.compra?.obra?.id === obraId)
        );
        setEmpleados(datosEmpleados); 
      } catch (error) {
        console.error("Error al cargar datos:", error);
      }

      setCargando(false);
    };

    if (id) {
      cargarDatos();
    }
  }, [id]);

  const manejarAsignar = async (e) => {
    e.preventDefault();
    setErrorAsignacion("");

    try {
      await AltaAsignacionEmpleado({
        fechaAsignacion,
        empleadoId: Number(empleadoSeleccionado),
        obraId: Number(id),
      });

      setEmpleadoSeleccionado("");
      setFechaAsignacion("");
      setMostrarFormAsignacion(false);

      const nuevasAsignaciones = await ObtenerAsignacionesEmpleado();
      setAsignaciones(nuevasAsignaciones.filter((a) => a.obra?.id === Number(id)));
    } catch (err) {
      setErrorAsignacion(err.message);
    }
  };

  if (cargando) return <p style={{ padding: "2rem" }}>Cargando detalle de la obra...</p>;
  if (!obra) return <p style={{ padding: "2rem" }}>Obra no encontrada.</p>;

  const porcentaje = obra.presupuesto > 0 ? (obra.costoActual / obra.presupuesto) * 100 : 0;
  const progresoAjustado = Math.min(porcentaje, 100);
  const colorBarra = porcentaje > 100 ? "#a8402f" : "#2f5233";

  return (
    <div style={estilos.contenedor}>
      <button onClick={() => router.push("/obras")} style={estilos.btnVolver}>
        &larr; Volver a obras
      </button>

      <h1 style={estilos.titulo}>Obra: {obra.nombre}</h1>

      <div style={estilos.tabsContenedor}>
        <button
          style={pestanaActiva === "info" ? estilos.tabActiva : estilos.tabInactiva}
          onClick={() => setPestanaActiva("info")}
        >
          Información general
        </button>
        <button
          style={pestanaActiva === "etapas" ? estilos.tabActiva : estilos.tabInactiva}
          onClick={() => setPestanaActiva("etapas")}
        >
          Etapas
        </button>
        <button
          style={pestanaActiva === "empleados" ? estilos.tabActiva : estilos.tabInactiva}
          onClick={() => setPestanaActiva("empleados")}
        >
          Empleados
        </button>
        <button
          style={pestanaActiva === "compras" ? estilos.tabActiva : estilos.tabInactiva}
          onClick={() => setPestanaActiva("compras")}
        >
          Compras
        </button>
        <button
          style={pestanaActiva === "presupuesto" ? estilos.tabActiva : estilos.tabInactiva}
          onClick={() => setPestanaActiva("presupuesto")}
        >
          Presupuesto
        </button>
      </div>

      <div style={estilos.contenido}>
        {pestanaActiva === "info" && (
          <div>
            <h2>Detalles</h2>
            <p><strong>Dirección:</strong> {obra.direccion}</p>
            <p><strong>Tipo:</strong> {nombresTipoObra[obra.tipoObra]}</p>
            <p><strong>Intervención:</strong> {nombresIntervencion[obra.intervencionObra]}</p>
            <p><strong>Estado:</strong> {nombresEstado[obra.estado]}</p>
            <p><strong>Fecha Inicio:</strong> {obra.fechaInicio?.split("T")[0]}</p>
            <p><strong>Fecha Final:</strong> {obra.fechaFinal?.split("T")[0]}</p>
          </div>
        )}

        {pestanaActiva === "etapas" && (
          <div>
            <h2>Etapas de la obra</h2>
            {etapas.length === 0 ? (
              <p>No hay etapas registradas.</p>
            ) : (
              <table>
                <thead>
                  <tr>
                    <th>Nombre</th>
                    <th>Descripción</th>
                    <th>Estado</th>
                    <th>Fecha Inicio</th>
                    <th>Fecha Fin</th>
                  </tr>
                </thead>
                <tbody>
                  {etapas.map((e) => (
                    <tr key={e.id}>
                      <td>{e.nombre}</td>
                      <td>{e.descripcion}</td>
                      <td>{nombresEstado[e.estado]}</td>
                      <td>{e.fechaInicio?.split("T")[0]}</td>
                      <td>{e.fechaFin?.split("T")[0]}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </div>
        )}

        {pestanaActiva === "empleados" && (
          <div>
            <h2>Empleados Asignados</h2>

            {!mostrarFormAsignacion && (
              <button
                onClick={() => setMostrarFormAsignacion(true)}
                style={estilos.botonAsignar}
              >
                + Asignar empleado
              </button>
            )}

            {mostrarFormAsignacion && (
              <form onSubmit={manejarAsignar} style={estilos.formAsignacion}>
                <div style={estilos.campoAsignacion}>
                  <label>Empleado</label>
                  <select
                    value={empleadoSeleccionado}
                    onChange={(e) => setEmpleadoSeleccionado(e.target.value)}
                    required
                    style={estilos.inputAsignacion}
                  >
                    <option value="">Seleccioná un empleado</option>
                    {empleados.map((emp) => (
                      <option key={emp.id} value={emp.id}>
                        {emp.nombre}
                      </option>
                    ))}
                  </select>
                </div>

                <div style={estilos.campoAsignacion}>
                  <label>Fecha de asignación</label>
                  <input
                    type="date"
                    value={fechaAsignacion}
                    onChange={(e) => setFechaAsignacion(e.target.value)}
                    required
                    style={estilos.inputAsignacion}
                  />
                </div>

                {errorAsignacion && <p style={estilos.errorAsignacion}>{errorAsignacion}</p>}

                <div style={estilos.accionesAsignacion}>
                  <button type="submit" style={estilos.botonAsignar}>
                    Confirmar
                  </button>
                  <button
                    type="button"
                    onClick={() => setMostrarFormAsignacion(false)}
                    style={estilos.botonCancelarAsignacion}
                  >
                    Cancelar
                  </button>
                </div>
              </form>
            )}

            {asignaciones.length === 0 ? (
              <p style={{ marginTop: "1rem" }}>No hay empleados asignados.</p>
            ) : (
              <table style={{ marginTop: "1rem" }}>
                <thead>
                  <tr>
                    <th>Empleado</th>
                    <th>Fecha Asignación</th>
                  </tr>
                </thead>
                <tbody>
                  {asignaciones.map((a) => (
                    <tr key={a.id}>
                      <td>{a.empleado?.nombre || "Empleado no encontrado"}</td>
                      <td>{a.fechaAsignacion?.split("T")[0]}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </div>
        )}

        {pestanaActiva === "compras" && (
          <div>
            <h2>Compras de Material</h2>
            {detallesCompra.length === 0 ? (
              <p>No hay compras registradas para esta obra.</p>
            ) : (
              <table>
                <thead>
                  <tr>
                    <th>Material</th>
                    <th>Cantidad</th>
                    <th>Precio</th>
                    <th>Fecha de compra</th>
                  </tr>
                </thead>
                <tbody>
                  {detallesCompra.map((d) => (
                    <tr key={d.id}>
                      <td>{d.material?.nombre || "Material no encontrado"}</td>
                      <td>{d.cantidad}</td>
                      <td>${d.precio}</td>
                      <td>{d.compra?.fecha?.split("T")[0]}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </div>
        )}

        {pestanaActiva === "presupuesto" && (
          <div>
            <h2>Presupuesto vs Costo Actual</h2>
            <div style={estilos.presupuestoInfo}>
              <p><strong>Presupuesto:</strong> ${obra.presupuesto}</p>
              <p><strong>Costo Actual:</strong> ${obra.costoActual}</p>
              <p>
                <strong>Diferencia:</strong> ${obra.presupuesto - obra.costoActual}
                {obra.presupuesto - obra.costoActual < 0 ? " (Sobregiro)" : " (A favor)"}
              </p>
            </div>

            <div style={estilos.barraFondo}>
              <div
                style={{
                  ...estilos.barraProgreso,
                  width: `${progresoAjustado}%`,
                  backgroundColor: colorBarra,
                }}
              >
                {porcentaje.toFixed(1)}%
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

const estilos = {
  contenedor: { padding: "2rem", fontFamily: "sans-serif" },
  btnVolver: {
    padding: "0.5rem 1rem",
    marginBottom: "1rem",
    cursor: "pointer",
    backgroundColor: "#f7f5f0",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
  },
  titulo: { marginBottom: "1.5rem" },
  tabsContenedor: {
    display: "flex",
    gap: "0.5rem",
    marginBottom: "1rem",
    borderBottom: "2px solid #e0dcd0",
  },
  tabInactiva: {
    padding: "0.5rem 1rem",
    cursor: "pointer",
    backgroundColor: "transparent",
    border: "none",
    borderBottom: "2px solid transparent",
    fontSize: "1rem",
  },
  tabActiva: {
    padding: "0.5rem 1rem",
    cursor: "pointer",
    backgroundColor: "transparent",
    border: "none",
    borderBottom: "2px solid #2f5233",
    color: "#2f5233",
    fontWeight: "bold",
    fontSize: "1rem",
  },
  contenido: {
    padding: "1.5rem",
    backgroundColor: "#fff",
    borderRadius: "8px",
    border: "1px solid #e0dcd0",
  },
  presupuestoInfo: { marginBottom: "1.5rem", fontSize: "1.1rem" },
  barraFondo: {
    width: "100%",
    backgroundColor: "#e0dcd0",
    borderRadius: "8px",
    overflow: "hidden",
    height: "30px",
  },
  barraProgreso: {
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    color: "white",
    fontWeight: "bold",
    fontSize: "0.9rem",
    transition: "width 0.3s ease",
  },
  botonAsignar: {
    padding: "0.5rem 1rem",
    backgroundColor: "#2f5233",
    color: "#fff",
    border: "none",
    borderRadius: "6px",
    cursor: "pointer",
    fontSize: "0.9rem",
    marginRight: "0.5rem",
  },
  botonCancelarAsignacion: {
    padding: "0.5rem 1rem",
    backgroundColor: "transparent",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
    cursor: "pointer",
    fontSize: "0.9rem",
    color: "#6b6558",
  },
  formAsignacion: {
    backgroundColor: "#f7f5f0",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
    padding: "1rem",
    marginBottom: "1rem",
    maxWidth: "400px",
  },
  campoAsignacion: {
    display: "flex",
    flexDirection: "column",
    gap: "0.35rem",
    marginBottom: "0.75rem",
  },
  inputAsignacion: {
    padding: "0.5rem",
    borderRadius: "6px",
    border: "1px solid #e0dcd0",
  },
  accionesAsignacion: {
    display: "flex",
    gap: "0.5rem",
  },
  errorAsignacion: {
    color: "#a8402f",
    backgroundColor: "#fdf0ee",
    border: "1px solid #f0c9c2",
    borderRadius: "6px",
    padding: "0.5rem 0.75rem",
    fontSize: "0.85rem",
    marginBottom: "0.5rem",
  },
};