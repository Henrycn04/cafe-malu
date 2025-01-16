<template>
  <div class="form-container">
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
          <button type="button" @click="mostrarModal('cliente')">Ingresar un nuevo cliente</button>
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
            id="cafe"
            required
            placeholder="Seleccione o busque un tipo de café"
          />
          <button type="button" @click="mostrarModal('cafe')">Ingresar un nuevo tipo de café</button>
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

      <!-- Botón para guardar -->
      <div class="form-group">
        <button type="submit" class="save-button">Guardar Venta</button>
      </div>
    </form>

    <!-- Modal para agregar cliente -->
    <div v-if="modalVisible === 'cliente'" class="modal-overlay">
      <div class="modal">
        <h2>Agregar Cliente</h2>
        <form @submit.prevent="guardarNuevoCliente">
          <div class="form-group">
            <label for="nombreCliente">Nombre</label>
            <input type="text" id="nombreCliente" v-model="nuevoCliente.nombre" required />
          </div>
          <div class="form-group">
            <label for="emailCliente">Email</label>
            <input type="email" id="emailCliente" v-model="nuevoCliente.email" required />
          </div>
          <div class="form-buttons">
            <button type="submit">Guardar</button>
            <button type="button" @click="cerrarModal">Cancelar</button>
          </div>
        </form>
      </div>
    </div>

    <!-- Modal para agregar café -->
    <div v-if="modalVisible === 'cafe'" class="modal-overlay">
      <div class="modal">
        <h2>Agregar Café</h2>
        <form @submit.prevent="guardarNuevoCafe">
          <div class="form-group">
            <label for="nombreCafe">Nombre</label>
            <input type="text" id="nombreCafe" v-model="nuevoCafe.nombre" required />
          </div>
          <div class="form-buttons">
            <button type="submit">Guardar</button>
            <button type="button" @click="cerrarModal">Cancelar</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import Multiselect from "vue-multiselect";
import "vue-multiselect/dist/vue-multiselect.min.css";
import axios from "axios";

export default {
  components: {
    Multiselect,
  },
  data() {
    return {
      clientes: [
        { id: 1, nombre: "Cliente 1" },
        { id: 2, nombre: "Cliente 2" },
        { id: 3, nombre: "Cliente Especial" },
      ],
      cafes: [
        { id: 1, nombre: "Café Arábica" },
        { id: 2, nombre: "Café Robusta" },
        { id: 3, nombre: "Café Geisha" },
      ],
      venta: {
        Cliente: null,
        Fecha: new Date().toISOString().split("T")[0],
        Cafe: null,
        Cantidad: 0,
        Pago: 0.0,
      },
      modalVisible: null,
      nuevoCliente: {
        nombre: "",
        email: "",
      },
      nuevoCafe: {
        nombre: "",
      },
    };
  },
  methods: {
    mostrarModal(tipo) {
      this.modalVisible = tipo;
    },
    cerrarModal() {
      this.modalVisible = null;
    },
    guardarNuevoCliente() {
      this.clientes.push({
        id: this.clientes.length + 1,
        nombre: this.nuevoCliente.nombre,
        email: this.nuevoCliente.email,
      });
      this.cerrarModal();
    },
    guardarNuevoCafe() {
      this.cafes.push({
        id: this.cafes.length + 1,
        nombre: this.nuevoCafe.nombre,
      });
      this.cerrarModal();
    },
    guardarVenta() {
      if (this.validarVenta()) {
        console.log("Venta guardada:", this.venta);
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
      axios.get(this.$backendAddress + "api/CompanyProfileData/UserCompanies", {

      })
      .then((response) => {
        this.userCompanies = response.data;
                       
      })
      .catch((error) => {
        console.error("Error obtaining user companies:", error);
      });
    },

    addNuevoCliente() {
      axios.post(this.$backendAddress + "api/UserData/updateData", {
        name: this.newData.name,
        emailAddress: this.newData.emailAddress,
        phoneNumber: this.newData.phoneNumber,
        UserID: this.userID
      }).then((response)=>{
        console.log(response.data);
        alert("Se actualizaron los datos correctamente");
        this.$router.push("UserProfile");
      }).catch((error)=>{
        console.log("Error al hacer update ", error);
        alert("Error al actualizar datos, volviendo al perfil");
        this.$router.push("UserProfile");
      })
    },
  },
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

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.modal {
  background-color: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  max-width: 400px;
  width: 100%;
}

.modal h2 {
  margin-top: 0;
  margin-bottom: 1rem;
}

.modal .form-buttons {
  display: flex;
  justify-content: space-between;
}
</style>
