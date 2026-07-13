const { faker } = require("@faker-js/faker");

const URL = "http://localhost:5156";

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

const cedula = () => faker.string.numeric(8);
const telefono = () => "09" + faker.string.numeric(7);
const random = (arr) => arr[Math.floor(Math.random() * arr.length)];
const fechaPasada = () => faker.date.past({ years: 2 }).toISOString().split("T")[0];
const fechaFutura = () => faker.date.future({ years: 2 }).toISOString().split("T")[0];

async function crearClientes(cantidad) {
  const creados = [];
  for (let i = 0; i < cantidad; i++) {
    const cliente = await post("Cliente", {
      nombre: faker.person.firstName(),
      apellido: faker.person.lastName(),
      cedula: cedula(),
      telefono: telefono(),
    });
    if (cliente) creados.push(cliente);
  }
  console.log(`${creados.length} clientes creados.`);
  return creados;
}

async function crearEmpleados(cantidad) {
  const creados = [];
  for (let i = 0; i < cantidad; i++) {
    const empleado = await post("Empleado", {
      nombre: `${faker.person.firstName()} ${faker.person.lastName()}`,
      cedula: cedula(),
      telefono: telefono(),
      cargo: Math.floor(Math.random() * 6),
      salario: faker.number.int({ min: 30000, max: 120000 }),
    });
    if (empleado) creados.push(empleado);
  }
  console.log(`${creados.length} empleados creados.`);
  return creados;
}

async function crearContratistas(cantidad) {
  const creados = [];
  for (let i = 0; i < cantidad; i++) {
    const contratista = await post("Contratista", {
      nombre: `${faker.person.firstName()} ${faker.person.lastName()}`,
      cedula: cedula(),
      especialidad: Math.floor(Math.random() * 6),
      telefono: telefono(),
    });
    if (contratista) creados.push(contratista);
  }
  console.log(`${creados.length} contratistas creados.`);
  return creados;
}

async function crearProveedores(cantidad) {
  const creados = [];
  for (let i = 0; i < cantidad; i++) {
    const proveedor = await post("Proveedor", {
      nombre: faker.company.name(),
      telefono: telefono(),
      direccion: faker.location.streetAddress(),
    });
    if (proveedor) creados.push(proveedor);
  }
  console.log(`${creados.length} proveedores creados.`);
  return creados;
}

async function crearMateriales(cantidad) {
  const materiales = ["Cemento", "Arena", "Ladrillo", "Hierro", "Pintura", "Cerámica", "Cal", "Madera", "Grava", "Yeso"];
  const creados = [];
  for (let i = 0; i < cantidad; i++) {
    const material = await post("Material", {
      nombre: `${random(materiales)} ${faker.commerce.productAdjective()}`,
      descripcion: faker.commerce.productDescription(),
      stock: faker.number.int({ min: 0, max: 500 }),
      precioUnitario: faker.number.int({ min: 50, max: 1000 }),
    });
    if (material) creados.push(material);
  }
  console.log(`${creados.length} materiales creados.`);
  return creados;
}

async function crearObras(cantidad, clientes) {
  const creados = [];
  for (let i = 0; i < cantidad; i++) {
    const obra = await post("Obra", {
      nombre: `${faker.company.name()} - ${random(["Torre", "Edificio", "Complejo", "Residencial"])}`,
      direccion: faker.location.streetAddress(),
      tipoObra: Math.floor(Math.random() * 4),
      intervencionObra: Math.floor(Math.random() * 4),
      estado: Math.floor(Math.random() * 6),
      fechaInicio: fechaPasada(),
      fechaFinal: fechaFutura(),
      presupuesto: faker.number.int({ min: 500000, max: 15000000 }),
      costoActual: faker.number.int({ min: 0, max: 500000 }),
    });
    if (obra) {
      if (clientes.length > 0) {
        const clienteAsociado = random(clientes);
        await fetch(`${URL}/Obra/${obra.id}/clientes/${clienteAsociado.id}`, { method: "POST" });
      }
      creados.push(obra);
    }
  }
  console.log(`${creados.length} obras creadas.`);
  return creados;
}

async function crearEtapas(cantidad, obras) {
  if (obras.length === 0) return;
  let contador = 0;
  for (let i = 0; i < cantidad; i++) {
    const obra = random(obras);
    const resultado = await post("EtapaObra", {
      nombre: random(["Excavación", "Cimientos", "Estructura", "Mampostería", "Instalaciones", "Terminaciones"]),
      descripcion: faker.lorem.sentence(),
      estado: Math.floor(Math.random() * 6),
      fechaInicio: fechaPasada(),
      fechaFin: fechaFutura(),
      obraId: obra.id,
    });
    if (resultado) contador++;
  }
  console.log(`${contador} etapas creadas.`);
}

async function crearGastos(cantidad, obras) {
  if (obras.length === 0) return;
  let contador = 0;
  for (let i = 0; i < cantidad; i++) {
    const obra = random(obras);
    const resultado = await post("Gasto", {
      descripcion: faker.commerce.productName(),
      fecha: fechaPasada(),
      monto: faker.number.int({ min: 1000, max: 200000 }),
      tipo: Math.floor(Math.random() * 5),
      obraId: obra.id,
    });
    if (resultado) contador++;
  }
  console.log(`${contador} gastos creados.`);
}

async function crearAsignaciones(cantidad, empleados, obras) {
  if (empleados.length === 0 || obras.length === 0) return;
  let contador = 0;
  for (let i = 0; i < cantidad; i++) {
    const resultado = await post("AsignacionEmpleado", {
      fechaAsignacion: fechaPasada(),
      empleadoId: random(empleados).id,
      obraId: random(obras).id,
    });
    if (resultado) contador++;
  }
  console.log(`${contador} asignaciones de empleados creadas.`);
}

async function crearComprasYDetalles(cantidad, obras, materiales, proveedores) {
  if (obras.length === 0 || materiales.length === 0 || proveedores.length === 0) return;
  let contadorCompras = 0;
  let contadorDetalles = 0;

  for (let i = 0; i < cantidad; i++) {
    const obra = random(obras);
    const compra = await post("CompraMaterial", {
      fecha: fechaPasada(),
      montoTotal: faker.number.int({ min: 5000, max: 200000 }),
      obraId: obra.id,
      proveedorId: random(proveedores).id,
    });

    if (compra) {
      contadorCompras++;
      const material = random(materiales);
      const detalle = await post("DetalleCompra", {
        cantidad: faker.number.int({ min: 1, max: 100 }),
        precio: faker.number.int({ min: 100, max: 5000 }),
        compraId: compra.id,
        materialId: material.id,
      });
      if (detalle) contadorDetalles++;
    }
  }

  console.log(`${contadorCompras} compras y ${contadorDetalles} detalles de compra creados.`);
}

  

async function main() {
  const clientes = await crearClientes(20);
  const empleados = await crearEmpleados(15);
  await crearContratistas(10);
  const proveedores = await crearProveedores(8);
  const materiales = await crearMateriales(15);
  const obras = await crearObras(12, clientes);
  await crearEtapas(30, obras);
  await crearGastos(25, obras);
  await crearAsignaciones(20, empleados, obras);
  await crearComprasYDetalles(15, obras, materiales,proveedores);
  

  console.log("Carga de datos completa.");
}



main();