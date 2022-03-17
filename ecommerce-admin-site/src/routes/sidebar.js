import {
  FiShoppingBag,
  FiUsers,
  FiList,
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
];

export default sidebar;
