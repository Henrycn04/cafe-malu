<template>
  <div class="form-container">
    <h1>Listado de Ventas</h1>
    <button @click="redirectToVerVentas">Ver Ventas</button>

    <h1>Agregar Venta</h1>
    <form class="add-sale horizontal-form" @submit.prevent="guardarVenta">
      <!-- Cliente -->
      <div class="form-group">
        <label for="cliente">Cliente</label>
        <div class="input-group">
          <multiselect
            v-model="venta.Cliente"
            :options="clientes"
            :searchable="true"
            :label="'nombre'"
            :track-by="'nombre'"
            :filter="customFilter"
            :multiple="false"
            placeholder="Seleccione o busque un cliente"
          />
          <button type="button" @click="mostrarFormularioCliente">Ingresar un nuevo cliente</button>
        </div>
      </div>

      <!-- Fecha -->
      <div class="form-group">
        <label for="fecha">Fecha</label>
        <input
          type="date"
          id="fecha"
          v-model="venta.Fecha"
          required
        />
      </div>

      <!-- Café -->
      <div class="form-group">
        <label for="cafe">Café</label>
        <div class="input-group">
          <Multiselect
            v-model="venta.Cafe"
            :options="cafes"
            :searchable="true"
            :label="'nombre'"
            :track-by="'nombre'"
            :filter="customFilter"
            :multiple="false"
            placeholder="Seleccione o busque un tipo de café"
          />
          <button type="button" @click="mostrarFormularioCafe">Ingresar un nuevo tipo de café</button>
        </div>
      </div>

      <!-- Cantidad -->
      <div class="form-group">
        <label for="cantidad">Cantidad</label>
        <input
          type="number"
          id="cantidad"
          v-model.number="venta.Cantidad"
          min="1"
          placeholder="Ingrese la cantidad"
          required
        />
      </div>

      <!-- Pago -->
      <div class="form-group">
        <label for="pago">Pago</label>
        <input
          type="number"
          id="pago"
          v-model.number="venta.Pago"
          step="0.01"
          min="0"
          placeholder="Ingrese el pago"
          required
        />
      </div>
      <div>
  <label>Total: </label>
  <span>{{ total.toFixed(2) }}</span>
</div>
      <!-- Botón para guardar -->
      <div class="form-group">
        <button type="submit" class="save-button">Guardar Venta</button>
      </div>
    </form>

    <!-- Modal para nuevo cliente -->
    <div v-if="mostrarModalCliente" class="modal">
      <div class="modal-content">
        <h2>Ingresar Nuevo Cliente</h2>
        <form @submit.prevent="guardarNuevoCliente">
          <div class="form-group">
            <label for="nombreCliente">Nombre</label>
            <input type="text" id="nombreCliente" v-model="nuevoCliente.Nombre" required />
          </div>
          <div class="form-group">
            <label for="telefonoCliente">Número Telefónico</label>
            <input type="text" id="telefonoCliente" v-model="nuevoCliente.NumeroTelefonico" required />
          </div>
          <div class="form-group">
            <button type="submit">Guardar Cliente</button>
            <button type="button" @click="cerrarFormularioCliente">Cancelar</button>
          </div>
        </form>
      </div>
    </div>

    <!-- Modal para nuevo café -->
    <div v-if="mostrarModalCafe" class="modal">
      <div class="modal-content">
        <h2>Ingresar Nuevo Café</h2>
        <form @submit.prevent="guardarNuevoCafe">
          <div class="form-group">
            <label for="nombreCafe">Nombre</label>
            <input type="text" id="nombreCafe" v-model="nuevoCafe.Nombre" required />
          </div>
          <div class="form-group">
            <label for="pesoCafe">Peso (kg)</label>
            <input type="number" id="pesoCafe" v-model.number="nuevoCafe.Peso" min="0.1" step="0.1" required />
          </div>
          <div class="form-group">
            <label for="especialidadCafe">Especialidad</label>
            <input type="checkbox" id="especialidadCafe" v-model="nuevoCafe.Especialidad" />
          </div>
          <div class="form-group">
            <label for="precioCafe">Precio Unitario</label>
            <input type="number" id="precioCafe" v-model.number="nuevoCafe.PrecioUnitario" min="0" step="0.01" required />
          </div>
          <div class="form-group">
            <button type="submit">Guardar Café</button>
            <button type="button" @click="cerrarFormularioCafe">Cancelar</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import Multiselect from 'vue-multiselect';
import 'vue-multiselect/dist/vue-multiselect.min.css';
import axios from "axios";
export default {
  components: {
    Multiselect,
  },
  data() {
    return {
      clientes: [],
      cafes: [],
      // Datos de la venta
      venta: {
        Cliente: null,
        Fecha: new Date().toISOString().split("T")[0],
        Cafe: null,
        Cantidad: 0,
        Pago: 0.0,
      },
      // Datos para nuevos formularios
      nuevoCliente: {
        Nombre: "",
        NumeroTelefonico: "",
      },
      nuevoCafe: {
        Nombre: "",
        Peso: 0.0,
        Especialidad: false,
        PrecioUnitario: 0.0,
      },
      mostrarModalCliente: false,
      mostrarModalCafe: false,
    };
  },
  computed: {
    total() {
    // Calcular el total en función de Cafe y Cantidad
    return this.venta.Cafe && this.venta.Cafe.precioUnitario && this.venta.Cantidad
      ? this.venta.Cafe.precioUnitario * this.venta.Cantidad
      : 0;
  },
  },
  methods: {
    redirectToVerVentas() {
      this.$router.push({ name: 'VerVentas' });
    },
    customFilter(option, search) {
      if (!search) return true;
      return option.label.toLowerCase().includes(search.toLowerCase());
    },
    mostrarFormularioCliente() {
      this.mostrarModalCliente = true;
    },
    cerrarFormularioCliente() {
      this.mostrarModalCliente = false;
    },
    guardarNuevoCliente() {
      if (this.nuevoCliente.Nombre && this.nuevoCliente.NumeroTelefonico) {
        this.addNuevoCliente();
        this.cerrarFormularioCliente();
      } else {
        alert("Complete todos los campos para agregar un nuevo cliente.");
      }
    },
    mostrarFormularioCafe() {
      this.mostrarModalCafe = true;
    },
    cerrarFormularioCafe() {
      this.mostrarModalCafe = false;
    },
    guardarNuevoCafe() {
      if (
        this.nuevoCafe.Nombre &&
        this.nuevoCafe.Peso > 0 &&
        this.nuevoCafe.PrecioUnitario >= 0
      ) {
        this.addNuevoCafe();
        this.cerrarFormularioCafe();
      } else {
        alert("Complete todos los campos para agregar un nuevo café.");
      }
    },
    guardarVenta() {
      if (this.validarVenta()) {
        this.addNuevaVenta();
        alert("Venta registrada exitosamente.");
        this.resetFormulario();
      } else {
        alert("Por favor, complete todos los campos correctamente.");
      }
    },
    validarVenta() {
      return (
        this.venta.Cliente &&
        this.venta.Cafe &&
        this.venta.Cantidad > 0 &&
        this.venta.Pago >= 0
      );
    },
    resetFormulario() {
      this.venta = {
        Cliente: null,
        Fecha: new Date().toISOString().split("T")[0],
        Cafe: null,
        Cantidad: 0,
        Pago: 0.0,
      };
    },
    getClientes() {
      axios.get(this.$backendAddress + "api/Ventas/ObtenerClientes")
        .then((response) => {
          this.clientes1 = response.data.clientes; // Verifica que sea un arreglo.
          this.clientes = [...this.clientes1]; // Verifica que sea un arreglo.
        })
        .catch((error) => {
          console.error("Error obteniendo clientes:", error);
          this.clientes = []; // Asegura que esté inicializado.
        });
    },
    getCafes() {
      axios.get(this.$backendAddress + "api/Ventas/ObtenerCafes")
        .then((response) => {
          this.cafes1 = response.data.cafes; // Verifica que sea un arreglo.
          this.cafes = [...this.cafes1]; // Verifica que sea un arreglo.
        })
        .catch((error) => {
          console.error("Error obteniendo cafés:", error);
          this.cafes = []; // Asegura que esté inicializado.
        });
    },
    addNuevoCliente() {
      console.log("Datos enviados:", {
        Nombre: this.nuevoCliente.Nombre,
        NumeroTelefonico: this.nuevoCliente.NumeroTelefonico
      });
      axios.post(this.$backendAddress + "api/Ventas/RegistrarCliente", {
        id: 0, // se crea solo
        nombre: this.nuevoCliente.Nombre,
        clienteBusqueda: this.nuevoCliente.Nombre,
        frecuencia: 0, // se calcula
        numeroTelefonico: this.nuevoCliente.NumeroTelefonico
      }).then(()=>{
        window.location.reload();
      }).catch((error)=>{
        console.error("Error en cliente: ", error.response ? error.response.data : error);
        window.location.reload();
      })
    },
    addNuevoCafe() {
      axios.post(this.$backendAddress + "api/Ventas/RegistrarCafe", {
        nombre: this.nuevoCafe.Nombre,
        peso: this.nuevoCafe.Peso,
        especialidad: this.nuevoCafe.Especialidad,
        precioUnitario: this.nuevoCafe.PrecioUnitario
      }).then(()=>{
        window.location.reload();
      }).catch((error)=>{
        console.error("Error en cafe: ", error.response ? error.response.data : error);
        window.location.reload();
      })
    },
    addNuevaVenta() {
  const ventaData = {
    idCliente: this.venta.Cliente.id,
    fecha: this.venta.Fecha,
    idCafe: this.venta.Cafe.id,
    cantidad: this.venta.Cantidad,
    pago: this.venta.Pago,
  };
  console.log("Venta: ", ventaData);
  axios.post(this.$backendAddress + "api/Ventas/RegistrarVenta", ventaData)
    .then(() => {
      
      window.location.reload();
    })
    .catch((error) => {
      console.error("Error en venta: ", error.response ? error.response.data : error);
      window.location.reload();
    });
}

    
  },
  mounted() {
    this.getClientes();
    this.getCafes();
  }
  
};
</script>

<style scoped>
.form-container {
  margin: 2rem auto;
  padding: 2rem;
  max-width: 800px;
  background-color: #f9f9f9;
  border: 1px solid #ddd;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 2rem;
  border-radius: 8px;
  max-width: 500px;
  width: 100%;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

h1 {
  text-align: center;
  font-size: 2rem;
  margin-bottom: 1.5rem;
  color: #333;
}

.add-sale {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.input-group {
  display: flex;
  align-items: center;
  gap: 1rem;
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

.save-button {
  align-self: center;
  padding: 0.75rem 2rem;
  font-size: 1.1rem;
}

input {
  padding: 0.5rem;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  width: 100%;
}
</style>
