"use client";

import React, { useState, useEffect } from "react";
import { IconoEditar, IconoEliminar, IconoVer } from "../../Components/iconos";
import {
  ObtenerMateriales,
  AltaMaterial,
  ModificarMaterial,
  BajaMaterial,
  ObtenerDetallesCompra,
  ObtenerComprasMaterial,
  ObtenerObras,
} from "../../Api/api";

const nuevoMaterial = () => ({
  nombre: "",
  descripcion: "",
  stock: "",
  precioUnitario: "",
});

export default function MaterialesPage() {
  const [materiales, setMateriales] = useState([]);
  const [formulario, setFormulario] = useState(nuevoMaterial());
  const [editandoId, setEditandoId] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [mostrarFormulario, setMostrarFormulario] = useState(false);
  const [error, setError] = useState("");
  const [busquedaFiltro, setBusquedaFiltro] = useState("");
  const [mostrarComprasDe, setMostrarComprasDe] = useState(null);
  const [detallesCompra, setDetallesCompra] = useState([]);
  const [comprasMaterial, setComprasMaterial] = useState([]);
  const [obras, setObras] = useState([]);


  const cargarDatosExtras = async () => {
    try {
      const [detalles, compras, obrasList] = await Promise.all([
        ObtenerDetallesCompra(),
        ObtenerComprasMaterial(),
        ObtenerObras(),
      ]);
      setDetallesCompra(detalles);
      setComprasMaterial(compras);
      setObras(obrasList);
    } catch (err) {
      console.error("Error cargando datos extras", err);
    }
  };

  const cargarMateriales = async () => {
    setCargando(true);
    try {
      const datos = await ObtenerMateriales();
      setMateriales(datos);
    } catch (err) {
      console.error(err);
    }
    setCargando(false);
  };

  useEffect(() => {
    cargarMateriales();
    cargarDatosExtras();
  }, []);

  const manejarCambio = (e) => {
    const { name, value } = e.target;
    setFormulario({ ...formulario, [name]: value });
  };

  const manejarSubmit = async (e) => {
    e.preventDefault();
    setError("");

    const materialAEnviar = {
      ...formulario,
      stock: Number(formulario.stock),
      precioUnitario: Number(formulario.precioUnitario),
    };

    try {
      if (editandoId) {
        await ModificarMaterial(editandoId, materialAEnviar);
      } else {
        await AltaMaterial(materialAEnviar);
      }

      setFormulario(nuevoMaterial());
      setEditandoId(null);
      setMostrarFormulario(false);
      cargarMateriales();
    } catch (err) {
      setError(err.message);
    }
  };

  const manejarEditar = (material) => {
    setFormulario({
      nombre: material.nombre,
      descripcion: material.descripcion,
      stock: material.stock,
      precioUnitario: material.precioUnitario,
    });
    setEditandoId(material.id);
    setError("");
    setMostrarFormulario(true);
  };

  const manejarEliminar = async (id) => {
    if (confirm("¿Estás seguro de eliminar este material?")) {
      try {
        await BajaMaterial(id);
        cargarMateriales();
      } catch (err) {
        setError(err.message);
        setMostrarFormulario(true);
      }
    }
  };

  const cancelarEdicion = () => {
    setFormulario(nuevoMaterial());
    setEditandoId(null);
    setError("");
    setMostrarFormulario(false);
  };

  return (
    <div>
      <div style={estilos.franja} />
      <span style={estilos.eyebrow}>Gestión de materiales</span>
      <h1 style={estilos.titulo}>Materiales</h1>

      <div style={estilos.barraAcciones}>
        {!mostrarFormulario && (
          <>
            <button
              onClick={() => setMostrarFormulario(true)}
              style={estilos.botonPrincipal}
            >
              + Nuevo material
            </button>
            <input
              type="text"
              placeholder="Buscar por nombre..."
              value={busquedaFiltro}
              onChange={(e) => setBusquedaFiltro(e.target.value)}
              style={estilos.filtro}
            />
          </>
        )}
      </div>

      {mostrarFormulario && (
        <form onSubmit={manejarSubmit} style={estilos.formulario}>
          <h2 style={estilos.tituloForm}>
            {editandoId ? "Editar material" : "Nuevo material"}
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
              <label style={estilos.label}>Descripción</label>
              <input
                type="text"
                name="descripcion"
                value={formulario.descripcion}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Stock</label>
              <input
                type="number"
                name="stock"
                value={formulario.stock}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>

            <div style={estilos.campo}>
              <label style={estilos.label}>Precio Unitario</label>
              <input
                type="number"
                name="precioUnitario"
                step="0.01"
                value={formulario.precioUnitario}
                onChange={manejarCambio}
                style={estilos.input}
                required
              />
            </div>
          </div>

          {error && <p style={estilos.error}>{error}</p>}

          <div style={estilos.accionesForm}>
            <button type="submit" style={estilos.botonPrincipal}>
              {editandoId ? "Guardar cambios" : "Crear material"}
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
                  <th>Descripción</th>
                  <th>Stock</th>
                  <th>Precio Unitario</th>
                  <th>Acciones</th>
                </tr>
              </thead>
              <tbody>
                {materiales.filter(m => {
                  return m.nombre.toLowerCase().includes(busquedaFiltro.toLowerCase());
                }).map((material) => (
                  <React.Fragment key={material.id}>
                    <tr>
                      <td>{material.id}</td>
                      <td>{material.nombre}</td>
                      <td>{material.descripcion}</td>
                      <td>{material.stock}</td>
                      <td>{material.precioUnitario}</td>
                      <td>
                        <div style={{ display: "flex", gap: "0.4rem", flexWrap: "nowrap" }}>
                          <button
                            onClick={() => setMostrarComprasDe(mostrarComprasDe === material.id ? null : material.id)}
                            style={estilos.botonIcono}
                            title={mostrarComprasDe === material.id ? "Ocultar compras" : "Ver compras"}
                          >
                            <IconoVer />
                          </button>
                          <button onClick={() => manejarEditar(material)} style={estilos.botonIcono} title="Editar">
                            <IconoEditar />
                          </button>
                          <button onClick={() => manejarEliminar(material.id)} style={estilos.botonIconoPeligro} title="Eliminar">
                            <IconoEliminar />
                          </button>
                          
                        </div>
                      </td>
                    </tr>
                    {mostrarComprasDe === material.id && (
                      <tr>
                        <td colSpan="6" style={estilos.celdaExpandida}>
                          <div style={estilos.contenidoExpandido}>
                            <h4 style={estilos.tituloCompras}>Compras de {material.nombre}</h4>
                            {(() => {
                              const detallesRelacionados = detallesCompra.filter(
                                (d) => d.material?.id === material.id
                              );
                              if (detallesRelacionados.length === 0) {
                                return <p style={estilos.textoSuave}>Este material no tiene compras registradas.</p>;
                              }

                              return (
                                <table style={estilos.tablaInterna}>
                                  <thead>
                                    <tr>
                                      <th style={estilos.thInterno}>Obra</th>
                                      <th style={estilos.thInterno}>Cantidad</th>
                                      <th style={estilos.thInterno}>Precio</th>
                                    </tr>
                                  </thead>
                                  <tbody>
                                    {detallesRelacionados.map((detalle) => (
                                      <tr key={detalle.id}>
                                        <td style={estilos.tdInterno}>
                                          {detalle.compra?.obra?.nombre || "Obra no encontrada"}
                                        </td>
                                        <td style={estilos.tdInterno}>{detalle.cantidad}</td>
                                        <td style={estilos.tdInterno}>${detalle.precio}</td>
                                      </tr>
                                    ))}
                                  </tbody>
                                </table>
                              );
                            })()}
                          </div>
                        </td>
                      </tr>
                    )}
                  </React.Fragment>
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
    backgroundColor: "#4a5d43",
    marginBottom: "0.75rem",
  },
  eyebrow: {
    fontSize: "0.75rem",
    textTransform: "uppercase",
    letterSpacing: "0.08em",
    color: "#4a5d43",
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
    flex: 1,
    maxWidth: "350px",
  },
  botonPrincipal: {
    padding: "0.6rem 1.2rem",
    backgroundColor: "#4a5d43",
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
    marginRight: "0.4rem",
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
  celdaExpandida: {
    padding: 0,
    backgroundColor: "#faf9f7",
    borderBottom: "1px solid #e0dcd0",
  },
  contenidoExpandido: {
    padding: "1rem",
    margin: "0 1rem 1rem 1rem",
    backgroundColor: "#fff",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
  },
  tituloCompras: {
    margin: "0 0 0.75rem 0",
    fontSize: "0.95rem",
    color: "#4a5d43",
  },
  textoSuave: {
    color: "#6b6558",
    fontSize: "0.85rem",
    margin: 0,
  },
  tablaInterna: {
    width: "100%",
    borderCollapse: "collapse",
  },
  thInterno: {
    textAlign: "left",
    padding: "0.4rem 0.5rem",
    borderBottom: "1px solid #e0dcd0",
    color: "#6b6558",
    fontSize: "0.8rem",
    fontWeight: 600,
  },
  tdInterno: {
    padding: "0.4rem 0.5rem",
    borderBottom: "1px solid #f0efeb",
    fontSize: "0.85rem",
  },
};
