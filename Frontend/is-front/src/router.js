// import Vue from 'vue';
// import Router from 'vue-router';
import { createWebHistory, createRouter } from 'vue-router';
// import Home from './components/Home.vue';
import Login from '@/components/Login.vue';
import Forget from '@/components/Forget.vue';
import StartPage from '@/components/StartPage.vue';

// Vue.use(Router);

const routes = [
    {
        path: '/',
        component: Login,
    },

    {
        path: '/ForgetPassword',
        component: Forget,
    },
    {
        path: '/StartPage',
        component: StartPage,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;



