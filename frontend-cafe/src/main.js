import { createApp } from 'vue'
import App from './App.vue'
import router from './router/router.js' // Importa el router


// import 'bootstrap/dist/css/custom.css'





const app = createApp(App);
app.use(router) // Usa el router en la aplicación

app.config.globalProperties.$backendAddress = 'https://localhost:7177/';


app.mount('#app'); 