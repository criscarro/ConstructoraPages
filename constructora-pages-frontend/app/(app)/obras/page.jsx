"use client";

import { useState, useEffect } from "react";
import { useSearchParams, useRouter } from "next/navigation";
import { IconoEditar, IconoEliminar, IconoVer } from "../../Components/iconos";

import {
  ObtenerObras,
  AltaObra,
  ModificarObra,
  BajaObra,
  ObtenerClientes,
  AsignarClienteAObra,
} from "../../Api/api";

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

const nuevaObra = () => ({
  nombre: "",
  direccion: "",
  tipoObra: 0,
  intervencionObra: 0,
  estado: 0,
  fechaInicio: "",
  fechaFinal: "",
  presupuesto: "",
  costoActual: "",
});

export default function Obras() {
  const [obras, setObras] = useState([]);
  const [clientes, setClientes] = useState([]);
  const [clientesSeleccionados, setClientesSeleccionados] = useState([]);
  const [formulario, setFormulario] = useState(nuevaObra());
  const [editandoId, setEditandoId] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [mostrarFormulario, setMostrarFormulario] = useState(false);
  const router = useRouter();
  const searchParams = useSearchParams();
  const [filtroEstado, setFiltroEstado] = useState(searchParams.get("estado") || "");
  const [filtroTipo, setFiltroTipo] = useState("");
  const [error, setError] = useState("");
  const [busquedaCliente, setBusquedaCliente] = useState("");

  const cargarObras = async () => {
    setCargando(true);
    const datos = await ObtenerObras();
    setObras(datos);
    setCargando(false);
  };

  const cargarClientes = async () => {
    const datos = await ObtenerClientes();
    setClientes(datos);
  };

  useEffect(() => {
    cargarObras();
    cargarClientes();
  }, []);

  const manejarCambio = (e) => {
    const { name, value } = e.target;
    setFormulario({ ...formulario, [name]: value });
  };

  const manejarCambioCliente = (id) => {
    setClientesSeleccionados((prev) =>
      prev.includes(id) ? prev.filter((c) => c !== id) : [...prev, id]
    );
  };

  const manejarSubmit = async (e) => {
  e.preventDefault();
  setError("");

  const obraAEnviar = {
    ...formulario,
    tipoObra: Number(formulario.tipoObra),
    intervencionObra: Number(formulario.intervencionObra),
    estado: Number(formulario.estado),
    presupuesto: Number(formulario.presupuesto),
    costoActual: Number(formulario.costoActual),
  };

  try {
    if (editandoId) {
      await ModificarObra(editandoId, obraAEnviar);
    } else {
      const obraCreada = await AltaObra(obraAEnviar);
      for (const clienteId of clientesSeleccionados) {
        await AsignarClienteAObra(obraCreada.id, clienteId);
      }
    }

    setFormulario(nuevaObra());
    setClientesSeleccionados([]);
    setEditandoId(null);
    setMostrarFormulario(false);
    cargarObras();
  } catch (err) {
    setError(err.message);
  }
};

  const manejarEditar = (obra) => {
    setFormulario({
      nombre: obra.nombre,
      direccion: obra.direccion,
      tipoObra: obra.tipoObra,
      intervencionObra: obra.intervencionObra,
      estado: obra.estado,
      fechaInicio: obra.fechaInicio?.split("T")[0] || "",
      fechaFinal: obra.fechaFinal?.split("T")[0] || "",
      presupuesto: obra.presupuesto,
      costoActual: obra.costoActual,
    });
    setEditandoId(obra.id);
    setMostrarFormulario(true);
  };

  const manejarEliminar = async (id) => {
    await BajaObra(id);
    cargarObras();
  };

  const cancelarEdicion = () => {
    setFormulario(nuevaObra());
    setClientesSeleccionados([]);
    setEditandoId(null);
    setMostrarFormulario(false);
  };

  const obrasFiltradas = obras.filter((obra) => {
    const pasaEstado = filtroEstado === "" || obra.estado === Number(filtroEstado);
    const pasaTipo = filtroTipo === "" || obra.tipoObra === Number(filtroTipo);
    return pasaEstado && pasaTipo;
  });

  return (
    <div>
      <div style={estilos.franja} />
      <span style={estilos.eyebrow}>Gestión de obras</span>
      <h1 style={estilos.titulo}>Obras</h1>

      <div style={estilos.barraAcciones}>
        {!mostrarFormulario && (
          <button
            onClick={() => setMostrarFormulario(true)}
            style={estilos.botonPrincipal}
          >
            + Nueva obra
          </button>
        )}

        {!mostrarFormulario && (
          <>
            <select
              value={filtroEstado}
              onChange={(e) => setFiltroEstado(e.target.value)}
              style={estilos.filtro}
            >
              <option value="">Todos los estados</option>
              {nombresEstado.map((nombre, i) => (
                <option key={i} value={i}>
                  {nombre}
                </option>
              ))}
            </select>

            <select
              value={filtroTipo}
              onChange={(e) => setFiltroTipo(e.target.value)}
              style={estilos.filtro}
            >
              <option value="">Todos los tipos</option>
              {nombresTipoObra.map((nombre, i) => (
                <option key={i} value={i}>
                  {nombre}
                </option>
              ))}
            </select>
          </>
        )}
</div>

      {mostrarFormulario && (
        <form onSubmit={manejarSubmit} style={estilos.formulario}>
          <h2 style={estilos.tituloForm}>
            {editandoId ? "Editar obra" : "Nueva obra"}
          </h2>

          <div style={estilos.grillaForm}>
            <div style={estilos.campo}>
              <label style={estilos.label}>Nombre</label>
              <input
                type="text"
                name="nombre"
                value={formulario.nombre}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Dirección</label>
              <input
                type="text"
                name="direccion"
                value={formulario.direccion}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Tipo de obra</label>
              <select
                name="tipoObra"
                value={formulario.tipoObra}
                onChange={manejarCambio}
                style={estilos.input}
              >
                {nombresTipoObra.map((nombre, i) => (
                  <option key={i} value={i}>
                    {nombre}
                  </option>
                ))}
              </select>
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Intervención</label>
              <select
                name="intervencionObra"
                value={formulario.intervencionObra}
                onChange={manejarCambio}
                style={estilos.input}
              >
                {nombresIntervencion.map((nombre, i) => (
                  <option key={i} value={i}>
                    {nombre}
                  </option>
                ))}
              </select>
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Estado</label>
              <select
                name="estado"
                value={formulario.estado}
                onChange={manejarCambio}
                style={estilos.input}
              >
                {nombresEstado.map((nombre, i) => (
                  <option key={i} value={i}>
                    {nombre}
                  </option>
                ))}
              </select>
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Fecha de inicio</label>
              <input
                type="date"
                name="fechaInicio"
                value={formulario.fechaInicio}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Fecha final</label>
              <input
                type="date"
                name="fechaFinal"
                value={formulario.fechaFinal}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Presupuesto</label>
              <input
                type="number"
                name="presupuesto"
                value={formulario.presupuesto}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Costo actual</label>
              <input
                type="number"
                name="costoActual"
                value={formulario.costoActual}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>
          </div>

          {!editandoId && (
            <div style={estilos.campoClientes}>
              <label style={estilos.label}>Clientes asociados</label>
              
              {/* Clientes seleccionados (siempre visibles) */}
              {clientesSeleccionados.length > 0 && (
                <div style={estilos.seleccionadosContainer}>
                  <p style={estilos.labelPequeno}>Seleccionados:</p>
                  <div style={estilos.listaSeleccionados}>
                    {clientes
                      .filter(c => clientesSeleccionados.includes(c.id))
                      .map(cliente => (
                        <div key={cliente.id} style={estilos.itemSeleccionado}>
                          <span>{cliente.nombre} {cliente.apellido} ({cliente.cedula})</span>
                          <button 
                            type="button" 
                            onClick={() => manejarCambioCliente(cliente.id)}
                            style={estilos.botonRemover}
                          >
                            x
                          </button>
                        </div>
                      ))}
                  </div>
                </div>
              )}

              {/* Buscador client-side. Si los clientes escalan a miles, esto debería moverse al backend */}
              <input
                type="text"
                placeholder="Buscar cliente por cédula..."
                value={busquedaCliente}
                onChange={(e) => setBusquedaCliente(e.target.value)}
                style={estilos.inputBusqueda}
              />
              
              <div style={estilos.listaClientes}>
                {clientes.length === 0 && (
                  <p style={estilos.textoSuave}>No hay clientes cargados todavía.</p>
                )}
                {clientes.length > 0 && busquedaCliente.trim() === "" && (
                  <p style={estilos.textoSuave}>Escriba una cédula para buscar clientes.</p>
                )}
                {busquedaCliente.trim() !== "" && clientes
                  .filter(c => c.cedula && c.cedula.includes(busquedaCliente.trim()))
                  .map((cliente) => (
                    <label key={cliente.id} style={estilos.checkboxLinea}>
                      <input
                        type="checkbox"
                        checked={clientesSeleccionados.includes(cliente.id)}
                        onChange={() => manejarCambioCliente(cliente.id)}
                      />
                      {cliente.nombre} {cliente.apellido} ({cliente.cedula})
                    </label>
                ))}
                {busquedaCliente.trim() !== "" && clientes.filter(c => c.cedula && c.cedula.includes(busquedaCliente.trim())).length === 0 && (
                  <p style={estilos.textoSuave}>No se encontraron coincidencias.</p>
                )}
              </div>
            </div>
          )}

          {error && <p style={estilos.error}>{error}</p>}

          <div style={estilos.accionesForm}>
            <button type="submit" style={estilos.botonPrincipal}>
              {editandoId ? "Guardar cambios" : "Crear obra"}
            </button>
            <button
              type="button"
              onClick={cancelarEdicion}
              style={estilos.botonSecundario}
            >
              Cancelar
            </button>
          </div>
        </form>
      )}

      {!mostrarFormulario && (
        <>
          <h2 style={estilos.tituloTabla}>Listado</h2>
            {cargando ? (
              <p>Cargando...</p>
            ) : (
              <table>
                <thead>
                  <tr>
                    <th>Id</th>
                    <th>Nombre</th>
                    <th>Dirección</th>
                    <th>Tipo</th>
                    <th>Estado</th>
                    <th>Presupuesto</th>
                    <th>Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {obrasFiltradas.map((obra) => (
                    <tr
                      key={obra.id}
                      onClick={() => router.push(`/obras/${obra.id}`)}
                      style={estilos.filaClickeable}
                    >
                      <td>{obra.id}</td>
                      <td>{obra.nombre}</td>
                      <td>{obra.direccion}</td>
                      <td>{nombresTipoObra[obra.tipoObra]}</td>
                      <td>{nombresEstado[obra.estado]}</td>
                      <td>{obra.presupuesto}</td>
                      <td onClick={(e) => e.stopPropagation()}>
                        <button
                          onClick={() => router.push(`/obras/${obra.id}`)}
                          style={estilos.botonIcono}
                          title="Ver detalle"
                        >
                          <IconoVer />
                        </button>
                        <button
                          onClick={() => manejarEditar(obra)}
                          style={estilos.botonIcono}
                          title="Editar"
                        >
                          <IconoEditar />
                        </button>
                        <button
                          onClick={() => manejarEliminar(obra.id)}
                          style={estilos.botonIconoPeligro}
                          title="Eliminar"
                        >
                          <IconoEliminar />
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </>
        )}
    </div>
  );
}

const estilos = {
  franja: {
    width: "40px",
    height: "4px",
    backgroundColor: "#2f5233",
    marginBottom: "0.75rem",
  },
  eyebrow: {
    fontSize: "0.75rem",
    textTransform: "uppercase",
    letterSpacing: "0.08em",
    color: "#2f5233",
    fontWeight: 600,
  },
  titulo: {
    fontSize: "1.8rem",
    marginTop: "0.25rem",
    marginBottom: "1.5rem",
  },
  barraAcciones: {
    display: "flex",
    gap: "0.75rem",
    marginBottom: "1.5rem",
    alignItems: "center",
  },
  filtro: {
    padding: "0.5rem 0.75rem",
    borderRadius: "6px",
    border: "1px solid #e0dcd0",
    backgroundColor: "#fff",
  },
  botonPrincipal: {
    padding: "0.6rem 1.2rem",
    backgroundColor: "#2f5233",
    color: "#fff",
    border: "none",
    borderRadius: "6px",
    fontSize: "0.9rem",
    fontWeight: 600,
  },
  botonSecundario: {
    padding: "0.6rem 1.2rem",
    backgroundColor: "transparent",
    color: "#6b6558",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
    fontSize: "0.9rem",
  },
  formulario: {
    backgroundColor: "#fff",
    border: "1px solid #e0dcd0",
    borderRadius: "8px",
    padding: "1.5rem",
    marginBottom: "2rem",
    maxWidth: "700px",
  },
  tituloForm: {
    fontSize: "1.1rem",
    marginBottom: "1rem",
  },
  grillaForm: {
    display: "grid",
    gridTemplateColumns: "1fr 1fr",
    gap: "1rem",
  },
  campo: {
    display: "flex",
    flexDirection: "column",
    gap: "0.35rem",
    marginTop: "1rem",
  },
  label: {
    fontSize: "0.85rem",
    color: "#6b6558",
  },
  input: {
    padding: "0.5rem",
    borderRadius: "6px",
    border: "1px solid #e0dcd0",
    fontSize: "0.9rem",
  },
  listaClientes: {
    display: "flex",
    flexDirection: "column",
    gap: "0.4rem",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
    padding: "0.75rem",
    maxHeight: "150px",
    overflowY: "auto",
  },
  checkboxLinea: {
    display: "flex",
    alignItems: "center",
    gap: "0.5rem",
    fontSize: "0.9rem",
  },
  textoSuave: {
    color: "#6b6558",
    fontSize: "0.85rem",
    margin: 0,
  },
  accionesForm: {
    display: "flex",
    gap: "0.75rem",
    marginTop: "1.5rem",
  },
  tituloTabla: {
    fontSize: "1.1rem",
    marginBottom: "0.75rem",
  },
  botonTabla: {
    padding: "0.35rem 0.75rem",
    marginRight: "0.5rem",
    backgroundColor: "#f7f5f0",
    border: "1px solid #e0dcd0",
    borderRadius: "4px",
    fontSize: "0.8rem",
  },
  botonTablaPeligro: {
    padding: "0.35rem 0.75rem",
    backgroundColor: "#fdf0ee",
    color: "#a8402f",
    border: "1px solid #f0c9c2",
    borderRadius: "4px",
    fontSize: "0.8rem",
  },
  botonIcono: {
    padding: "0.4rem",
    marginRight: "0.4rem",
    backgroundColor: "#f7f5f0",
    border: "1px solid #e0dcd0",
    borderRadius: "4px",
    cursor: "pointer",
    display: "inline-flex",
    alignItems: "center",
    justifyContent: "center",
    color: "#555",
  },
  botonIconoPeligro: {
    padding: "0.4rem",
    backgroundColor: "#fdf0ee",
    border: "1px solid #f0c9c2",
    borderRadius: "4px",
    cursor: "pointer",
    display: "inline-flex",
    alignItems: "center",
    justifyContent: "center",
    color: "#a8402f",
},
  error: {
    color: "#a8402f",
    backgroundColor: "#fdf0ee",
    border: "1px solid #f0c9c2",
    borderRadius: "6px",
    padding: "0.6rem 0.9rem",
    fontSize: "0.85rem",
    marginTop: "1rem",
  },
  campoClientes: {
    display: "flex",
    flexDirection: "column",
    gap: "0.5rem",
    marginTop: "1.5rem",
    gridColumn: "1 / -1",
  },
  seleccionadosContainer: {
    backgroundColor: "#f9f8f6",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
    padding: "0.75rem",
    marginBottom: "0.5rem",
  },
  labelPequeno: {
    fontSize: "0.8rem",
    color: "#6b6558",
    margin: "0 0 0.5rem 0",
    fontWeight: 600,
  },
  listaSeleccionados: {
    display: "flex",
    flexDirection: "column",
    gap: "0.4rem",
  },
  itemSeleccionado: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    fontSize: "0.9rem",
    backgroundColor: "#fff",
    padding: "0.4rem 0.6rem",
    borderRadius: "4px",
    border: "1px solid #e0dcd0",
  },
  botonRemover: {
    backgroundColor: "transparent",
    border: "none",
    color: "#a8402f",
    cursor: "pointer",
    fontWeight: "bold",
    fontSize: "0.9rem",
    padding: "0 0.2rem",
  },
  inputBusqueda: {
    padding: "0.5rem",
    borderRadius: "6px",
    border: "1px solid #e0dcd0",
    fontSize: "0.9rem",
    width: "100%",
  },
};