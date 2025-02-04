<template>
    <div>
        <button @click="redirectToVerVentas">Ingresar Venta</button>
      <h2>Lista de Ventas</h2>
      <table border="1">
        <thead>
          <tr>
            <th>ID</th>
            <th>Cliente</th>
            <th>Fecha</th>
            <th>Semana</th>
            <th>Café</th>
            <th>Cantidad</th>
            <th>Precio Total</th>
            <th>Pago</th>
            <th>Saldo</th>
            <th>Debe</th>
            <th>Peso Total</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="venta in ventas" :key="venta.id">
            <td>{{ venta.id }}</td>
            <td>{{ venta.idCliente }}</td>
            <td>{{ new Date(venta.fecha).toLocaleDateString() }}</td>
            <td>{{ venta.semana }}</td>
            <td>{{ venta.idCafe }}</td>
            <td>{{ venta.cantidad }}</td>
            <td>{{ venta.precioTotal.toFixed(2) }}</td>
            <td>{{ venta.pago.toFixed(2) }}</td>
            <td>{{ venta.saldo.toFixed(2) }}</td>
            <td>{{ venta.debe ? "Sí" : "No" }}</td>
            <td>{{ venta.pesoTotal.toFixed(2) }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  
  export default {

    data() {
      return {
        ventas: [], // Lista de ventas
      };
    },
    methods: {
        redirectToVerVentas() {
      this.$router.push({ name: 'HelloWorld' });
    },
      async fetchVentas() {
        try {
            axios.get(this.$backendAddress + "api/Ventas/ObtenerVentas")
                .then((response) => {
                this.ventas1 = response.data.ventas; // Verifica que sea un arreglo.
                this.ventas = [...this.ventas1]; // Verifica que sea un arreglo.
                console.log("Ventas: ", this.ventas);
                })
                .catch((error) => {
                console.error("Error obteniendo ventas:", error);
                this.ventas = []; // Asegura que esté inicializado.
                });
        } catch (error) {
          console.error("Error al obtener las ventas:", error);
        }
      },
    },
    created() {
      this.fetchVentas();
    },
  };
  </script>
  
  <style scoped>
  table {
    width: 100%;
    border-collapse: collapse;
  }
  
  th,
  td {
    padding: 8px;
    text-align: left;
  }
  
  th {
    background-color: #f4f4f4;
  }
  button {
  padding: 0.5rem 1rem;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}
  </style>