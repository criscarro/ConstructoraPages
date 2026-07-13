const URL = "http://localhost:5156";

const manejarRespuestaAltaModificacion = async (res) => {
  const isJson = res.headers.get("content-type")?.includes("application/json");
  const data = isJson ? await res.json() : null;

  if (!res.ok) {
    throw new Error(
      (data && data.mensaje) || "Ocurrió un error al procesar la solicitud."
    );
  }
  return data;
};

// --- OBRAS ---
export const ObtenerObras = async () => {
  const response = await fetch(`${URL}/Obra`).then((res) => res.json());
  return response;
};

export const ObtenerObraPorId = async (id) => {
  const response = await fetch(`${URL}/Obra/${id}`).then((res) => res.json());
  return response;
};

export const AltaObra = async (obra) => {
  const response = await fetch(`${URL}/Obra`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(obra),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const ModificarObra = async (id, obra) => {
  const response = await fetch(`${URL}/Obra/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(obra),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const BajaObra = async (id) => {
  const response = await fetch(`${URL}/Obra/${id}`, {
    method: "DELETE",
  });
  return response.ok;
};

// --- EMPLEADOS ---
export const ObtenerEmpleados = async () => {
  const response = await fetch(`${URL}/Empleado`).then((res) => res.json());
  return response;
};

export const AltaEmpleado = async (empleado) => {
  const response = await fetch(`${URL}/Empleado`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(empleado),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const ModificarEmpleado = async (id, empleado) => {
  const response = await fetch(`${URL}/Empleado/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(empleado),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const BajaEmpleado = async (id) => {
  const response = await fetch(`${URL}/Empleado/${id}`, {
    method: "DELETE",
  });
  return response.ok;
};

// --- CLIENTES ---
export const ObtenerClientes = async () => {
  const response = await fetch(`${URL}/Cliente`).then((res) => res.json());
  return response;
};

export const AltaCliente = async (cliente) => {
  const response = await fetch(`${URL}/Cliente`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(cliente),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const ModificarCliente = async (id, cliente) => {
  const response = await fetch(`${URL}/Cliente/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(cliente),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const BajaCliente = async (id) => {
  const response = await fetch(`${URL}/Cliente/${id}`, {
    method: "DELETE",
  });
  return response.ok;
};

// --- CONTRATISTAS ---
export const ObtenerContratistas = async () => {
  const response = await fetch(`${URL}/Contratista`).then((res) => res.json());
  return response;
};

export const AltaContratista = async (contratista) => {
  const response = await fetch(`${URL}/Contratista`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(contratista),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const ModificarContratista = async (id, contratista) => {
  const response = await fetch(`${URL}/Contratista/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(contratista),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const BajaContratista = async (id) => {
  const response = await fetch(`${URL}/Contratista/${id}`, {
    method: "DELETE",
  });
  return response.ok;
};

// --- PROVEEDORES ---
export const ObtenerProveedores = async () => {
  const response = await fetch(`${URL}/Proveedor`).then((res) => res.json());
  return response;
};

export const AltaProveedor = async (proveedor) => {
  const response = await fetch(`${URL}/Proveedor`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(proveedor),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const ModificarProveedor = async (id, proveedor) => {
  const response = await fetch(`${URL}/Proveedor/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(proveedor),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const BajaProveedor = async (id) => {
  const response = await fetch(`${URL}/Proveedor/${id}`, {
    method: "DELETE",
  });
  return response.ok;
};

// --- MATERIALES ---
export const ObtenerMateriales = async () => {
  const response = await fetch(`${URL}/Material`).then((res) => res.json());
  return response;
};

export const AltaMaterial = async (material) => {
  const response = await fetch(`${URL}/Material`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(material),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const ModificarMaterial = async (id, material) => {
  const response = await fetch(`${URL}/Material/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(material),
  });
  return manejarRespuestaAltaModificacion(response);
};

export const BajaMaterial = async (id) => {
  const response = await fetch(`${URL}/Material/${id}`, {
    method: "DELETE",
  });
  return response.ok;
};

// --- RELACIONES PARA DETALLE DE OBRA ---
export const ObtenerEtapasObra = async () => {
  const response = await fetch(`${URL}/EtapaObra`).then((res) => res.json());
  return response;
};

export const ObtenerAsignacionesEmpleado = async () => {
  const response = await fetch(`${URL}/AsignacionEmpleado`).then((res) => res.json());
  return response;
};

export const ObtenerComprasMaterial = async () => {
  const response = await fetch(`${URL}/CompraMaterial`).then((res) => res.json());
  return response;
};

export const ObtenerDetallesCompra = async () => {
  const response = await fetch(`${URL}/DetalleCompra`).then((res) => res.json());
  return response;
};

// --- RELACIÓN CLIENTE-OBRA ---
export const AsignarClienteAObra = async (obraId, clienteId) => {
  const response = await fetch(`${URL}/Obra/${obraId}/clientes/${clienteId}`, {
    method: "POST",
  });
  if (!response.ok) {
    const isJson = response.headers.get("content-type")?.includes("application/json");
    const data = isJson ? await response.json() : null;
    throw new Error((data && data.mensaje) || "Ocurrió un error al procesar la solicitud.");
  }
  return response.ok;
};

export const AltaAsignacionEmpleado = async (asignacion) => {
  const response = await fetch(`${URL}/AsignacionEmpleado`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(asignacion),
  });
  const data = await response.json();
  if (!response.ok) {
    throw new Error(data.mensaje || "Error al asignar el empleado.");
  }
  return data;
};