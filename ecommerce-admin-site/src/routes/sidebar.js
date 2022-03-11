import {
  FiShoppingBag,
  FiUsers,
  FiList,
  FiSettings,
} from 'react-icons/fi';
/**
 * ⚠ These are used just to render the Sidebar!
 * You can include any link here, local or external.
 *
 * If you're looking to actual Router routes, go to
 * `routes/index.js`
 */
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
