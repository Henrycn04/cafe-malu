import { createRouter, createWebHistory } from 'vue-router'
import HelloWorld from '@/components/HelloWorld.vue'
import VerVentas from '@/components/VerVentas.vue'
const routes = [
    {
        path: '/',
        name: 'HelloWorld',
        component: HelloWorld
    },
    {
        path: '/VerVentas',
        name: 'VerVentas',
        component: VerVentas
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router