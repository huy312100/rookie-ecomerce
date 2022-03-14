import {
  FiShoppingBag,
  FiUsers,
  FiList,
  FiSettings,
} from 'react-icons/fi';

const sidebar = [
  {
    path: '/products',
    icon: FiShoppingBag,
    name: 'Products',
  },
  {
    path: '/category',
    icon: FiList,
    name: 'Category',
  },
  {
    path: '/customers',
    icon: FiUsers,
    name: 'Customers',
  },
  {
    path: '/setting',
    icon: FiSettings,
    name: 'Setting',
  },
];

export default sidebar;
