import requests from './HttpService';

const CategoryServices = {
  getAllCategory() {
    return requests.get('/category/all');
  },

  getCategoryById(id) {
    return requests.get(`/category/${id}`);
  },

  addCategory(body) {
    return requests.post('/category', body);
  },

  updateCategory(body) {
    return requests.put('/category', body);
  },

  deleteCategory(categoryId) {
    return requests.delete(`/category/${categoryId}`);
  },
};

export default CategoryServices;
