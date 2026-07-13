const CARGOS = {
  JEFE: "Jefe",
  MAESTRO: "MaestroDeObra",
  ALBANIL: "Albanil",
  ARQUITECTO: "Arquitecto",
  INGENIERO: "Ingeniero",
  ADMINISTRATIVO: "Administrativo",
};

export const iniciarSesion = (usuario, contrasena) => {
  const usuarioNormalizado = usuario.trim().toUpperCase();
  const contrasenaNormalizada = contrasena.trim().toUpperCase();

  if (usuarioNormalizado !== contrasenaNormalizada) {
    return { exito: false, mensaje: "Usuario y contraseña deben coincidir." };
  }

  const cargo = CARGOS[usuarioNormalizado];
  if (!cargo) {
    return { exito: false, mensaje: "Usuario no reconocido." };
  }

  localStorage.setItem("cargo", cargo);
  localStorage.setItem("usuario", usuarioNormalizado);
  return { exito: true, cargo };
};

export const cerrarSesion = () => {
  localStorage.removeItem("cargo");
  localStorage.removeItem("usuario");
};

export const obtenerCargo = () => {
  if (typeof window === "undefined") return null;
  return localStorage.getItem("cargo");
};