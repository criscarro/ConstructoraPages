"use client";

import { useState, useEffect } from "react";
import { IconoEditar, IconoEliminar } from "../../Components/iconos";
import {
  ObtenerEmpleados,
  AltaEmpleado,
  ModificarEmpleado,
  BajaEmpleado,
} from "../../Api/api";

const nuevoEmpleado = () => ({
  nombre: "",
  cedula: "",
  telefono: "",
  cargo: 0,
  salario: "",
});

const nombresCargo = [
  "Jefe",
  "Maestro de obra",
  "Albañil",
  "Arquitecto",
  "Ingeniero",
  "Administrativo",
];

export default function EmpleadosPage() {
  const [empleados, setEmpleados] = useState([]);
  const [formulario, setFormulario] = useState(nuevoEmpleado());
  const [editandoId, setEditandoId] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [mostrarFormulario, setMostrarFormulario] = useState(false);
  const [error, setError] = useState("");
  const [filtroCargo, setFiltroCargo] = useState("");
  const [busquedaFiltro, setBusquedaFiltro] = useState("");

  const cargarEmpleados = async () => {
    setCargando(true);
    try {
      const datos = await ObtenerEmpleados();
      setEmpleados(datos);
    } catch (err) {
      console.error(err);
    }
    setCargando(false);
  };

  useEffect(() => {
    cargarEmpleados();
  }, []);

  const manejarCambio = (e) => {
    const { name, value } = e.target;
    setFormulario({ ...formulario, [name]: value });
  };

  const manejarSubmit = async (e) => {
    e.preventDefault();
    setError("");

    const empleadoAEnviar = {
      ...formulario,
      cargo: Number(formulario.cargo),
      salario: Number(formulario.salario),
    };

    try {
      if (editandoId) {
        await ModificarEmpleado(editandoId, empleadoAEnviar);
      } else {
        await AltaEmpleado(empleadoAEnviar);
      }

      setFormulario(nuevoEmpleado());
      setEditandoId(null);
      setMostrarFormulario(false);
      cargarEmpleados();
    } catch (err) {
      setError(err.message);
    }
  };

  const manejarEditar = (empleado) => {
    setFormulario({
      nombre: empleado.nombre,
      cedula: empleado.cedula,
      telefono: empleado.telefono,
      cargo: empleado.cargo,
      salario: empleado.salario,
    });
    setEditandoId(empleado.id);
    setError("");
    setMostrarFormulario(true);
  };

  const manejarEliminar = async (id) => {
    if (confirm("¿Estás seguro de eliminar este empleado?")) {
      try {
        await BajaEmpleado(id);
        cargarEmpleados();
      } catch (err) {
        setError(err.message);
        setMostrarFormulario(true);
      }
    }
  };

  const cancelarEdicion = () => {
    setFormulario(nuevoEmpleado());
    setEditandoId(null);
    setError("");
    setMostrarFormulario(false);
  };

  const empleadosFiltrados = empleados.filter((emp) => {
    const term = busquedaFiltro.toLowerCase();
    const pasaFiltroTexto = emp.nombre.toLowerCase().includes(term) || emp.cedula.includes(term);
    const pasaCargo = filtroCargo === "" || emp.cargo === Number(filtroCargo);
    return pasaFiltroTexto && pasaCargo;
  });

  return (
    <div>
      <div style={estilos.franja} />
      <span style={estilos.eyebrow}>Gestión de empleados</span>
      <h1 style={estilos.titulo}>Empleados</h1>

      <div style={estilos.barraAcciones}>
        {!mostrarFormulario && (
          <button
            onClick={() => setMostrarFormulario(true)}
            style={estilos.botonPrincipal}
          >
            + Nuevo empleado
          </button>
        )}

        {!mostrarFormulario && (
          <>
            <input
              type="text"
              placeholder="Buscar por nombre o cédula..."
              value={busquedaFiltro}
              onChange={(e) => setBusquedaFiltro(e.target.value)}
              style={estilos.filtroTexto}
            />
            <select
              value={filtroCargo}
              onChange={(e) => setFiltroCargo(e.target.value)}
              style={estilos.filtro}
            >
              <option value="">Todos los cargos</option>
              {nombresCargo.map((nombre, i) => (
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
            {editandoId ? "Editar empleado" : "Nuevo empleado"}
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
              <label style={estilos.label}>Cédula</label>
              <input
                type="text"
                name="cedula"
                value={formulario.cedula}
                onChange={(e) => {
                  const soloNumeros = e.target.value.replace(/\D/g, "").slice(0, 8);
                  setFormulario({ ...formulario, cedula: soloNumeros });
                }}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Teléfono</label>
              <input
                type="text"
                name="telefono"
                value={formulario.telefono}
                onChange={(e) => {
                  const soloNumeros = e.target.value.replace(/\D/g, "");
                  setFormulario({ ...formulario, telefono: soloNumeros });
                }}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Cargo</label>
              <select
                name="cargo"
                value={formulario.cargo}
                onChange={manejarCambio}
                style={estilos.input}
              >
                {nombresCargo.map((nombre, i) => (
                  <option key={i} value={i}>
                    {nombre}
                  </option>
                ))}
              </select>
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Salario</label>
              <input
                type="number"
                name="salario"
                value={formulario.salario}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>
          </div>

          {error && <p style={estilos.error}>{error}</p>}

          <div style={estilos.accionesForm}>
            <button type="submit" style={estilos.botonPrincipal}>
              {editandoId ? "Guardar cambios" : "Crear empleado"}
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
                  <th>Cédula</th>
                  <th>Teléfono</th>
                  <th>Cargo</th>
                  <th>Salario</th>
                  <th>Acciones</th>
                </tr>
              </thead>
              <tbody>
                {empleadosFiltrados.map((empleado) => (
                  <tr key={empleado.id}>
                    <td>{empleado.id}</td>
                    <td>{empleado.nombre}</td>
                    <td>{empleado.cedula}</td>
                    <td>{empleado.telefono}</td>
                    <td>{nombresCargo[empleado.cargo]}</td>
                    <td>{empleado.salario}</td>
                    <td>
                      <button onClick={() => manejarEditar(empleado)} style={estilos.botonIcono} title="Editar">
                        <IconoEditar />
                      </button>
                      <button
                        onClick={() => manejarEliminar(empleado.id)}
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
    backgroundColor: "#b8860b",
    marginBottom: "0.75rem",
  },
  eyebrow: {
    fontSize: "0.75rem",
    textTransform: "uppercase",
    letterSpacing: "0.08em",
    color: "#b8860b",
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
  filtroTexto: {
    padding: "0.5rem 0.75rem",
    borderRadius: "6px",
    border: "1px solid #e0dcd0",
    backgroundColor: "#fff",
    flex: 1,
    maxWidth: "350px",
  },
  botonPrincipal: {
    padding: "0.6rem 1.2rem",
    backgroundColor: "#b8860b",
    color: "#fff",
    border: "none",
    borderRadius: "6px",
    fontSize: "0.9rem",
    fontWeight: 600,
    cursor: "pointer",
  },
  botonSecundario: {
    padding: "0.6rem 1.2rem",
    backgroundColor: "transparent",
    color: "#6b6558",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
    fontSize: "0.9rem",
    cursor: "pointer",
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
    cursor: "pointer",
  },
  botonTablaPeligro: {
    padding: "0.35rem 0.75rem",
    backgroundColor: "#fdf0ee",
    color: "#a8402f",
    border: "1px solid #f0c9c2",
    borderRadius: "4px",
    fontSize: "0.8rem",
    cursor: "pointer",
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
};
