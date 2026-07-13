"use client";

import { useState, useEffect } from "react";
import { IconoEditar, IconoEliminar } from "../../Components/iconos";
import {
  ObtenerContratistas,
  AltaContratista,
  ModificarContratista,
  BajaContratista,
} from "../../Api/api";

const nuevoContratista = () => ({
  nombre: "",
  cedula: "",
  telefono: "",
  especialidad: 0,
});

const nombresEspecialidad = [
  "Electricista",
  "Pintor",
  "Carpintero",
  "Soldador",
  "Herrero",
  "Sanitario",
];

export default function ContratistasPage() {
  const [contratistas, setContratistas] = useState([]);
  const [formulario, setFormulario] = useState(nuevoContratista());
  const [editandoId, setEditandoId] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [mostrarFormulario, setMostrarFormulario] = useState(false);
  const [error, setError] = useState("");
  const [filtroEspecialidad, setFiltroEspecialidad] = useState("");
  const [busquedaFiltro, setBusquedaFiltro] = useState("");

  const cargarContratistas = async () => {
    setCargando(true);
    try {
      const datos = await ObtenerContratistas();
      setContratistas(datos);
    } catch (err) {
      console.error(err);
    }
    setCargando(false);
  };

  useEffect(() => {
    cargarContratistas();
  }, []);

  const manejarCambio = (e) => {
    const { name, value } = e.target;
    setFormulario({ ...formulario, [name]: value });
  };

  const manejarSubmit = async (e) => {
    e.preventDefault();
    setError("");

    const contratistaAEnviar = {
      ...formulario,
      especialidad: Number(formulario.especialidad),
    };

    try {
      if (editandoId) {
        await ModificarContratista(editandoId, contratistaAEnviar);
      } else {
        await AltaContratista(contratistaAEnviar);
      }

      setFormulario(nuevoContratista());
      setEditandoId(null);
      setMostrarFormulario(false);
      cargarContratistas();
    } catch (err) {
      setError(err.message);
    }
  };

  const manejarEditar = (contratista) => {
    setFormulario({
      nombre: contratista.nombre,
      cedula: contratista.cedula,
      telefono: contratista.telefono,
      especialidad: contratista.especialidad,
    });
    setEditandoId(contratista.id);
    setError("");
    setMostrarFormulario(true);
  };

  const manejarEliminar = async (id) => {
    if (confirm("¿Estás seguro de eliminar este contratista?")) {
      try {
        await BajaContratista(id);
        cargarContratistas();
      } catch (err) {
        setError(err.message);
        setMostrarFormulario(true);
      }
    }
  };

  const cancelarEdicion = () => {
    setFormulario(nuevoContratista());
    setEditandoId(null);
    setError("");
    setMostrarFormulario(false);
  };

  const contratistasFiltrados = contratistas.filter((cont) => {
    const term = busquedaFiltro.toLowerCase();
    const pasaFiltroTexto = cont.nombre.toLowerCase().includes(term) || cont.cedula.includes(term);
    const pasaEspecialidad = filtroEspecialidad === "" || cont.especialidad === Number(filtroEspecialidad);
    return pasaFiltroTexto && pasaEspecialidad;
  });

  return (
    <div>
      <div style={estilos.franja} />
      <span style={estilos.eyebrow}>Gestión de contratistas</span>
      <h1 style={estilos.titulo}>Contratistas</h1>

      <div style={estilos.barraAcciones}>
        {!mostrarFormulario && (
          <button
            onClick={() => setMostrarFormulario(true)}
            style={estilos.botonPrincipal}
          >
            + Nuevo contratista
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
              value={filtroEspecialidad}
              onChange={(e) => setFiltroEspecialidad(e.target.value)}
              style={estilos.filtro}
            >
              <option value="">Todas las especialidades</option>
              {nombresEspecialidad.map((nombre, i) => (
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
            {editandoId ? "Editar contratista" : "Nuevo contratista"}
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
              <label style={estilos.label}>Especialidad</label>
              <select
                name="especialidad"
                value={formulario.especialidad}
                onChange={manejarCambio}
                style={estilos.input}
              >
                {nombresEspecialidad.map((nombre, i) => (
                  <option key={i} value={i}>
                    {nombre}
                  </option>
                ))}
              </select>
            </div>
          </div>

          {error && <p style={estilos.error}>{error}</p>}

          <div style={estilos.accionesForm}>
            <button type="submit" style={estilos.botonPrincipal}>
              {editandoId ? "Guardar cambios" : "Crear contratista"}
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
                  <th>Especialidad</th>
                  <th>Acciones</th>
                </tr>
              </thead>
              <tbody>
                {contratistasFiltrados.map((contratista) => (
                  <tr key={contratista.id}>
                    <td>{contratista.id}</td>
                    <td>{contratista.nombre}</td>
                    <td>{contratista.cedula}</td>
                    <td>{contratista.telefono}</td>
                    <td>{nombresEspecialidad[contratista.especialidad]}</td>
                    <td>
                      <button onClick={() => manejarEditar(contratista)} style={estilos.botonIcono} title="Editar">
                        <IconoEditar />
                      </button>
                      <button
                        onClick={() => manejarEliminar(contratista.id)}
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
    backgroundColor: "#b5541f",
    marginBottom: "0.75rem",
  },
  eyebrow: {
    fontSize: "0.75rem",
    textTransform: "uppercase",
    letterSpacing: "0.08em",
    color: "#b5541f",
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
    backgroundColor: "#b5541f",
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
