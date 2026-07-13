"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { iniciarSesion } from "../lib/auth";

export default function Login() {
  const [usuario, setUsuario] = useState("");
  const [contrasena, setContrasena] = useState("");
  const [error, setError] = useState("");
  const router = useRouter();

  const manejarSubmit = (e) => {
    e.preventDefault();
    const resultado = iniciarSesion(usuario, contrasena);
    if (resultado.exito) {
      router.push("/");
    } else {
      setError(resultado.mensaje);
    }
  };

  return (
    <div style={estilos.contenedor}>
      <div style={estilos.tarjeta}>
        <h1 style={estilos.titulo}>Pages &amp; Familia</h1>
        <p style={estilos.subtitulo}>Ingresá con tu usuario de cargo</p>

        <form onSubmit={manejarSubmit}>
          <div style={estilos.campo}>
            <label htmlFor="usuario" style={estilos.label}>Usuario</label>
            <input
              id="usuario"
              type="text"
              value={usuario}
              onChange={(e) => setUsuario(e.target.value.toUpperCase())}
              style={estilos.input}
              autoComplete="username"
              required
            />
          </div>

          <div style={estilos.campo}>
            <label htmlFor="contrasena" style={estilos.label}>Contraseña</label>
            <input
              id="contrasena"
              type="password"
              value={contrasena}
              onChange={(e) => setContrasena(e.target.value.toUpperCase())}
              style={estilos.input}
              autoComplete="current-password"
              required
            />
          </div>

          {error && <p style={estilos.error}>{error}</p>}

          <button type="submit" style={estilos.boton}>
            Ingresar
          </button>
        </form>
      </div>
    </div>
  );
}

const estilos = {
  contenedor: {
    minHeight: "100vh",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    backgroundColor: "#f5f5f5",
    fontFamily: "sans-serif",
  },
  tarjeta: {
    backgroundColor: "#ffffff",
    padding: "2.5rem",
    borderRadius: "8px",
    boxShadow: "0 1px 4px rgba(0,0,0,0.15)",
    width: "100%",
    maxWidth: "360px",
  },
  titulo: {
    fontSize: "1.5rem",
    marginBottom: "0.25rem",
    textAlign: "center",
    color: "#1a1a1a",
  },
  subtitulo: {
    fontSize: "0.9rem",
    color: "#555",
    marginBottom: "1.5rem",
    textAlign: "center",
  },
  campo: {
    marginBottom: "1rem",
    display: "flex",
    flexDirection: "column",
    gap: "0.35rem",
  },
  label: {
    color: "#333",
    fontSize: "0.9rem",
  },
  input: {
    padding: "0.55rem",
    borderRadius: "4px",
    border: "1px solid #ccc",
    fontSize: "1rem",
    color: "#1a1a1a",
    backgroundColor: "#ffffff",
  },
  boton: {
    width: "100%",
    padding: "0.65rem",
    marginTop: "0.5rem",
    backgroundColor: "#2f5233",
    color: "#fff",
    border: "none",
    borderRadius: "4px",
    fontSize: "1rem",
    cursor: "pointer",
  },
  error: {
    color: "#c0392b",
    fontSize: "0.85rem",
    marginBottom: "0.5rem",
  },
};