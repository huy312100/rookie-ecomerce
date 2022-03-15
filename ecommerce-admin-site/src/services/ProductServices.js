import requests from './HttpService';

const ProductServices = {
  getAllProducts() {
    return requests.get('/product/all');
  },

  addProduct(body) {
    return requests.post('/products/add', body);
  },

  updateProduct(id, body) {
    return requests.put(`/products/${id}`, body);
  },

  deleteProduct(id) {
    return requests.delete(`/products/${id}`);
  },
};

export default ProductServices;
