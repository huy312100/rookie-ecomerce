import requests from './HttpService';
import requestsFormData from './HttpProductFormDataService';

const ProductServices = {
  getAllProducts() {
    return requests.get('/product/all');
  },

  getProductById(id) {
    return requests.get(`/product/${id}`);
  },

  addProduct(body) {
    return requestsFormData.post('/product', body);
  },

  updateProduct(body) {
    return requestsFormData.put('/product', body);
  },

  deleteProduct(productId) {
    return requests.delete(`/product/${productId}`);
  },
};

export default ProductServices;
