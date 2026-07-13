"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import { ObtenerObras } from "../Api/api";

const estadosInfo = [
  { valor: 0, nombre: "Pendiente", color: "#c17f2c" },
  { valor: 1, nombre: "En planificación", color: "#2c5f8a" },
  { valor: 2, nombre: "En ejecución", color: "#2f5233" },
  { valor: 3, nombre: "Pausa", color: "#6b6558" },
  { valor: 4, nombre: "Finalizada", color: "#4a5d43" },
  { valor: 5, nombre: "Cancelada", color: "#a8402f" },
];

export default function Dashboard() {
  const [obras, setObras] = useState([]);
  const [cargando, setCargando] = useState(true);

  useEffect(() => {
    const cargar = async () => {
      const datos = await ObtenerObras();
      setObras(datos);
      setCargando(false);
    };
    cargar();
  }, []);

  const contarPorEstado = (estado) =>
    obras.filter((o) => o.estado === estado).length;

  if (cargando) return <p>Cargando...</p>;

  return (
    <div>
      <span style={estilos.eyebrow}>Panel general</span>
      <h1 style={estilos.titulo}>Pages &amp; Familia</h1>
      <p style={estilos.subtitulo}>
        Tocá cualquier tarjeta para ver las obras de ese estado
      </p>

      <Link href="/obras" style={{ textDecoration: "none" }}>
        <div style={estilos.tarjetaDestacada}>
          <p style={estilos.numeroDestacado}>{obras.length}</p>
          <p style={estilos.etiquetaDestacada}>Obras totales — ver todas</p>
        </div>
      </Link>

      <div style={estilos.grilla}>
        {estadosInfo.map(({ valor, nombre, color }) => (
          <Link
            key={valor}
            href={`/obras?estado=${valor}`}
            style={{ textDecoration: "none" }}
          >
            <div style={estilos.tarjeta}>
              <div style={{ ...estilos.barraColor, backgroundColor: color }} />
              <p style={{ ...estilos.numero, color }}>{contarPorEstado(valor)}</p>
              <p style={estilos.etiqueta}>{nombre}</p>
            </div>
          </Link>
        ))}
      </div>
    </div>
  );
}

const estilos = {
  eyebrow: {
    fontSize: "0.75rem",
    textTransform: "uppercase",
    letterSpacing: "0.08em",
    color: "#c17f2c",
    fontWeight: 600,
  },
  titulo: {
    fontSize: "2rem",
    marginTop: "0.25rem",
    marginBottom: "0.25rem",
  },
  subtitulo: {
    color: "#6b6558",
    marginBottom: "1.5rem",
  },
  tarjetaDestacada: {
    backgroundColor: "#2f5233",
    borderRadius: "8px",
    padding: "1.5rem",
    marginBottom: "1.5rem",
    maxWidth: "900px",
    cursor: "pointer",
    transition: "opacity 0.15s",
  },
  numeroDestacado: {
    fontSize: "2.5rem",
    fontWeight: 700,
    margin: 0,
    lineHeight: 1.1,
    color: "#fff",
  },
  etiquetaDestacada: {
    fontSize: "0.9rem",
    color: "#d7e3d8",
    margin: "0.35rem 0 0 0",
  },
  grilla: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fit, minmax(170px, 1fr))",
    gap: "1rem",
    maxWidth: "900px",
  },
  tarjeta: {
    position: "relative",
    backgroundColor: "#ffffff",
    border: "1px solid #e0dcd0",
    borderRadius: "6px",
    padding: "1.25rem 1rem",
    display: "flex",
    flexDirection: "column",
    gap: "0.35rem",
    cursor: "pointer",
    transition: "transform 0.15s, box-shadow 0.15s",
  },
  barraColor: {
    position: "absolute",
    top: 0,
    left: 0,
    width: "100%",
    height: "4px",
    borderRadius: "6px 6px 0 0",
  },
  numero: {
    fontSize: "2.25rem",
    fontWeight: 700,
    margin: "0.5rem 0 0 0",
    lineHeight: 1.1,
  },
  etiqueta: {
    fontSize: "0.85rem",
    color: "#6b6558",
    margin: 0,
  },
};