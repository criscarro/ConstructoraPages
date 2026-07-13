"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { obtenerCargo, cerrarSesion } from "../Lib/auth";

const opcionesPorCargo = {
  Jefe: ["obras", "clientes", "empleados", "contratistas", "proveedores", "materiales"],
  MaestroDeObra: ["obras", "empleados", "contratistas", "materiales"],
  Arquitecto: ["obras", "contratistas", "materiales"],
  Ingeniero: ["obras", "contratistas", "materiales"],
  Administrativo: ["obras", "clientes", "empleados", "contratistas", "proveedores", "materiales"],
  Albanil: ["obras", "materiales"],
};

const etiquetas = {
  obras: "Obras",
  clientes: "Clientes",
  empleados: "Empleados",
  contratistas: "Contratistas",
  proveedores: "Proveedores",
  materiales: "Materiales",
};

export default function Sidebar() {
  const [cargo, setCargo] = useState(null);
  const router = useRouter();

  useEffect(() => {
    setCargo(obtenerCargo());
  }, []);

  const manejarCerrarSesion = () => {
    cerrarSesion();
    router.push("/login");
  };

  if (!cargo) return null;

  const opciones = opcionesPorCargo[cargo] || [];

  return (
    <div style={estilos.sidebar}>
      <h2 style={estilos.titulo}>Pages &amp; Familia</h2>
      <p style={estilos.cargo}>{cargo}</p>

      <nav style={estilos.nav}>
        <Link href="/" style={estilos.link}>
            Inicio
        </Link>
        {opciones.map((opcion) => (
            <Link key={opcion} href={`/${opcion}`} style={estilos.link}>
            {etiquetas[opcion]}
            </Link>
        ))}
      </nav>

      <button onClick={manejarCerrarSesion} style={estilos.boton}>
        Cerrar sesión
      </button>
    </div>
  );
}

const estilos = {
  sidebar: {
    width: "220px",
    height: "100vh",
    position: "sticky",
    top: 0,
    backgroundColor: "#2f5233",
    color: "#fff",
    padding: "1.5rem 1rem",
    display: "flex",
    flexDirection: "column",
    fontFamily: "sans-serif",
  },
  titulo: {
    fontSize: "1.1rem",
    marginBottom: "0.25rem",
  },
  cargo: {
    fontSize: "0.8rem",
    color: "#c8dcc8",
    marginBottom: "1.5rem",
  },
  nav: {
    display: "flex",
    flexDirection: "column",
    gap: "0.5rem",
    flex: 1,
    overflowY: "auto",
  },
  link: {
    color: "#fff",
    textDecoration: "none",
    padding: "0.5rem 0.75rem",
    borderRadius: "4px",
  },
  boton: {
    marginTop: "1rem",
    padding: "0.5rem",
    backgroundColor: "#1e3a21",
    color: "#fff",
    border: "none",
    borderRadius: "4px",
    cursor: "pointer",
    flexShrink: 0,
  },
};