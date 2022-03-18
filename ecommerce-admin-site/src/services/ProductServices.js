import requests from './HttpService';

const ProductServices = {
  getAllProducts() {
    return requests.get('/product/all');
  },

  getProductById(id) {
    return requests.get(`/product/${id}`);
  },

  addProduct(body) {
    return requests.post('/product', body);
  },

  updateProduct(body) {
    return requests.put('/product', body);
  },

  deleteProduct(productId) {
    return requests.delete(`/product/${productId}`);
  },
};

export default ProductServices;
