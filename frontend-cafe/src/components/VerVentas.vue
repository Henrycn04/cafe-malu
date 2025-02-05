<template>
    <div>
        <button @click="redirectToVerVentas">Ingresar Venta</button>
      <h2>Lista de Ventas</h2>
      <div class="container mt-4">
        <div class="filters mb-4 w-100">
          <div class="row g-3">

            <div class="col-12 col-md-3">
              <label for="allCompanies">Cliente:</label>
              <Multiselect
                v-model="this.filtros.cliente"
                :options="listaClientes.map(c => c.nombre)"
                :multiple="true"
                :close-on-select="false"
                :clear-on-select="false"
                placeholder="Seleccione cliente"              
              ></multiselect>
            </div>

            <div class="col-12 col-md-3">
              <label for="fechaInicio">Fecha (Inicio):</label>
              <input
                  v-model="this.filtros.fechaInicio"
                  type="date"
                  class="form-control"
                  id="fechaInicio"
              />
            </div>
            
            <div class="col-12 col-md-3">
              <label for="fechaFinal">Fecha (Fin):</label>
              <input
                  v-model="this.filtros.fechaFinal"
                  type="date"
                  class="form-control"
                  id="fechaFinal"
              />
            </div>

            <div class="col-12 col-md-3">
              <label for="cafe">Café:</label>
              <Multiselect
                v-model="this.filtros.cafe"
                :options="listaCafes.map(c => c.nombre)"
                :multiple="true"
                :close-on-select="false"
                :clear-on-select="false"
                placeholder="Seleccione cafe"              
              ></multiselect>
            </div>

            <div class="col-12 col-md-3">
              <label style="margin-bottom: 40px;">Precio: </label>
              <div ref="sliderPrecio"></div>
            </div>

            <div class="col-12 col-md-3">
              <label style="margin-bottom: 40px;">Peso: </label>
              <div ref="sliderPeso"></div>
            </div>

            <div class="col-12 col-md-3">
                <label style="margin-bottom: 40px;">Deudor:</label>
                <input type="checkbox" v-model="filtros.deudor"/>
            </div>

          </div>
          <div>
              <button class="btn btn-success" @click="aplicarFiltros" style="margin-top: 40px;" >Aplicar Filtros</button>
              
              <button class="btn btn-danger ms-2" @click="limpiarFiltros" style="margin-top: 40px; margin-left: 10px;">Limpiar Filtros</button>
          </div>                        
        </div>
      </div>
      
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
          <tr v-for="venta in ventasFiltradas" :key="venta.id">
            <td>{{ venta.id }}</td>
            <td>{{ venta.cliente }}</td>
            <td>{{ new Date(venta.fecha).toLocaleDateString() }}</td>
            <td>{{ venta.semana }}</td>
            <td>{{ venta.cafe }}</td>
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
  import 'nouislider/dist/nouislider.css';
  import noUiSlider from 'nouislider';
  import Multiselect from 'vue-multiselect';
  import 'vue-multiselect/dist/vue-multiselect.min.css';

  export default {
    components: {
      Multiselect,
    },
    data() {
      return {
        listaCafes: [],
        listaClientes: [],
        ventas: [],
        ventasCompletas: [],
        ventasFiltradas: [],
        filtros: {
          cliente: [],
          fechaInicio: null,
          fechaFinal: null,
          cafe: [],
          inicioPrecio: 0,
          finalPrecio: 0,
          deudor: false,
          inicioPeso: 0,
          finalPeso: 0,
        },
        maxPrecio: 0,
        maxPeso: 0,
        minPrecio: 0,
        minPeso: 0,
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
                this.ventas = response.data.ventas;                
                this.getClientes();
                })
                .catch((error) => {
                console.error("Error obteniendo ventas:", error);
                this.ventas = [];
                });
        } catch (error) {
          console.error("Error al obtener las ventas:", error);
        }
      },
      getClientes() {
        axios.get(this.$backendAddress + "api/Ventas/ObtenerClientes")
          .then((response) => {
            this.listaClientes = response.data.clientes;
            this.getCafes();
          })
          .catch((error) => {
            console.error("Error obteniendo clientes:", error);
            this.listaClientes = [];
          });
      },
      getCafes() {
        axios.get(this.$backendAddress + "api/Ventas/ObtenerCafes")
          .then((response) => {
            this.listaCafes = response.data.cafes;
            this.getVentasCompletas();
          })
          .catch((error) => {
            console.error("Error obteniendo cafés:", error);
            this.listaCafes = [];
          });
      },
      getVentasCompletas() {
        this.ventasCompletas = this.ventas.map(venta => {
          const cliente = this.listaClientes.find(c => c.id === venta.idCliente);
          const cafe = this.listaCafes.find(c => c.id === venta.idCafe);
          return {
            id: venta.id,
            cliente: cliente ? cliente.nombre : "Desconocido",
            fecha: venta.fecha,
            semana: venta.semana,
            cafe: cafe ? cafe.nombre : "Desconocido",
            cantidad: venta.cantidad,
            precioTotal: venta.precioTotal,
            pago: venta.pago,
            saldo: venta.saldo,
            debe: venta.debe,
            pesoTotal: venta.pesoTotal
          };
        });
        this.ventasFiltradas = [...this.ventasCompletas];
        this.crearSliders();
      },
      crearSliders() {
        ({ min: this.minPrecio, max: this.maxPrecio } = this.getMinMax(this.ventas, 'precioTotal'));
        ({ min: this.minPeso, max: this.maxPeso } = this.getMinMax(this.ventas, 'pesoTotal'));
        
        this.createSlider(this.$refs.sliderPrecio, this.minPrecio, this.maxPrecio, this.filtros.inicioPrecio, this.filtros.finalPrecio);
        this.$refs.sliderPrecio.noUiSlider.on('update', (values) => {
            this.filtros.inicioPrecio = parseInt(values[0], 10);
            this.filtros.finalPrecio = parseInt(values[1], 10);
        });
        
        this.createSlider(this.$refs.sliderPeso, this.minPeso, this.maxPeso, this.filtros.inicioPeso, this.filtros.finalPeso);
        this.$refs.sliderPeso.noUiSlider.on('update', (values) => {
            this.filtros.inicioPeso = parseInt(values[0], 10);
            this.filtros.finalPeso = parseInt(values[1], 10);
        });
      },
      limpiarFiltros() {
        this.filtros = {
          cliente: 0,
          fechaInicio: null,
          fechaFinal: null,
          cafe: 0,
          precio: 0,
          deudor: false,
          peso: 0,
        }
        this.ventasFiltradas = this.ventasCompletas;
      },
      aplicarFiltros() {
        let filtrado = this.ventasCompletas;
        if (Array.isArray(this.filtros.cliente) && this.filtros.cliente.length > 0) {
            filtrado = filtrado.filter(venta =>
            typeof venta.cliente === 'string' &&
            this.filtros.cliente.some(ventaI => venta.cliente.includes(ventaI))
            );
        }
        if (Array.isArray(this.filtros.cafe) && this.filtros.cafe.length > 0) {
            filtrado = filtrado.filter(venta =>
            typeof venta.cafe === 'string' &&
            this.filtros.cafe.some(cafeI => venta.cafe.includes(cafeI))
            );
        }
        if (this.filtros.deudor) {
          filtrado = filtrado.filter(venta => venta.debe === true);
        }
        if (this.filtros.fechaInicio) {
            filtrado = filtrado.filter(venta =>
            venta.fecha !== null && new Date(venta.fecha) >= new Date(this.filtros.fechaInicio)
            );
        }
        if (this.filtros.fechaFinal) {
            filtrado = filtrado.filter(venta =>
            venta.fecha !== null && new Date(venta.fecha) <= new Date(this.filtros.fechaFinal)
            );
        }
        this.ventasFiltradas = filtrado;
      },
      getMinMax(ventas, property) {
        if (!ventas || ventas.length === 0) {
            console.warn('No orders available to process.');
            return { min: null, max: null };
        }
        if (!ventas.every(venta => Object.prototype.hasOwnProperty.call(venta, property))) {
            console.warn(`La propiedad "${property}" no existe en todos los objetos de orders.`);
            return { min: null, max: null };
        }
        let min = Infinity;
        let max = -Infinity;
        ventas.forEach(venta => {
            const value = venta[property];
            if (value < min) {
                min = value;
            }
            if (value > max) {
                max = value;
            }
        });
        this.min = min;
        this.max = max;
        return { min, max };
      },
      createSlider(sliderElement, minValue, maxValue) {
        maxValue = maxValue + 1;
        minValue = minValue - 1;
        const slider = sliderElement;
        noUiSlider.create(slider, {
            start: [minValue, maxValue],
            connect: true,
            range: {
                'min': minValue,
                'max': maxValue
            },
            step: 1,
            tooltips: true,
            format: {
                to: (value) => Math.round(value),
                from: (value) => Number(value)
            }
        });
        const sliderBase = slider.querySelector('.noUi-base');
        const sliderConnect = slider.querySelector('.noUi-connect');
        if (sliderBase) {
            sliderBase.style.background = '#e0e0e0';
            sliderBase.style.transform = 'scaleX(0.9)';
            slider.style.background = 'transparent';
        }
        if (sliderConnect) {
            sliderConnect.style.background = '#f07800';
        }
        const sliderHandles = slider.querySelectorAll('.noUi-handle');
        sliderHandles.forEach(handle => {
            handle.style.backgroundColor = '#000000';
            handle.style.borderRadius = '15%';
        });
        sliderHandles.forEach(handle => {
            handle.style.transform = 'scale(0.9)';
        });
        const resetButton = document.getElementById('resetButton');
        if (resetButton) {
            resetButton.addEventListener('click', () => {
                slider.noUiSlider.set([minValue, maxValue]);
            });
        }
      },
    },
    mounted() {
      this.fetchVentas();
    }

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

.filters {
border: 1px solid #ddd;
padding: 15px;
border-radius: 5px;
}
.table {
margin-top: 20px;
border-collapse: collapse;
}
.thead-light {
background-color: #f8f9fa;
}
.col-10 {
height: calc(50vh - 100px);
}
.table thead th {
        text-align: left;
        background-color: #b97a3a;
        font-weight: bold;
        padding: 4px 8px;
    }
    .table thead th span{
        color: white;
    }
    .table-header {
        display: flex; 
        align-items: center;
        justify-content: space-between;
    }
  .th_button {
    width: auto;
    height: auto;
    padding-top: 2px;
    padding-bottom: 2px;
    padding-left: 6px;
    padding-right: 6px;
    margin: 0;
    border: none;
    background-color: #b97a3a; 
    color: white;
    text-align: center;
    font-weight: bold;
    cursor: pointer;
  }
  th {
    margin: 0;
  }
  </style>