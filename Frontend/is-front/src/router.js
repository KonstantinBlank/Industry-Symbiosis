import { createWebHistory, createRouter } from 'vue-router';
import Login from './components/Login.vue';
import Home from './components/Home.vue';
import AddEnterprise from './components/AddEnterprise.vue'

const routes = [
    {
        path: '/',
        component: Home
    },
    {
        path: '/Login',
        component: Login,
    },
    {
        path: '/Add',
        component: AddEnterprise,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;