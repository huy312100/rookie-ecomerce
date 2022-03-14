import { lazy } from 'react';

// use lazy for better code splitting
const Products = lazy(() => import('../pages/Products'));
// const ProductDetails = lazy(() => import('../pages/ProductDetails'));
const Category = lazy(() => import('../pages/Category'));
const Customers = lazy(() => import('../pages/Customers'));
const Page404 = lazy(() => import('../pages/404'));


const routes = [
  {
    path: '/products',
    component: Products,
  },
  // {
  //   path: '/product/:id',
  //   component: ProductDetails,
  // },
  {
    path: '/category',
    component: Category,
  },
  {
    path: '/customers',
    component: Customers,
  },
  {
    path: '/404',
    component: Page404,
  },
];

export default routes;
