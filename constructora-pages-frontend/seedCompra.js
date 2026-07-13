const { faker } = require("@faker-js/faker");

const URL = "http://localhost:5156";

const get = async (endpoint) => {
  const res = await fetch(`${URL}/${endpoint}`);
  return res.json();
};

const post = async (endpoint, body) => {
  const res = await fetch(`${URL}/${endpoint}`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  });
  const data = await res.json();
  if (!res.ok) {
    console.error(`Error en ${endpoint}:`, data.mensaje || data);
    return null;
  }
  return data;
};

const random = (arr) => arr[Math.floor(Math.random() * arr.length)];
const fechaPasada = () => faker.date.past({ years: 2 }).toISOString().split("T")[0];

async function main() {
  const materiales = await get("Material");
  const obras = await get("Obra");
  const proveedores = await get("Proveedor");

  if (materiales.length === 0 || obras.length === 0 || proveedores.length === 0) {
    console.log("Necesitás tener al menos un material, una obra y un proveedor cargados antes de correr esto.");
    return;
  }

  let contadorCompras = 0;
  let contadorDetalles = 0;

  for (const material of materiales) {
    const cantidadDeCompras = 1 + Math.floor(Math.random() * 2); // entre 1 y 2 compras por material

    for (let i = 0; i < cantidadDeCompras; i++) {
      const compra = await post("CompraMaterial", {
        fecha: fechaPasada(),
        montoTotal: faker.number.int({ min: 5000, max: 200000 }),
        obraId: random(obras).id,
        proveedorId: random(proveedores).id,
      });

      if (compra) {
        contadorCompras++;
        const detalle = await post("DetalleCompra", {
          cantidad: faker.number.int({ min: 1, max: 100 }),
          precio: faker.number.int({ min: 100, max: 5000 }),
          compraId: compra.id,
          materialId: material.id,
        });
        if (detalle) contadorDetalles++;
      }
    }
  }

  console.log(`${contadorCompras} compras y ${contadorDetalles} detalles creados, cubriendo ${materiales.length} materiales.`);
}

main();