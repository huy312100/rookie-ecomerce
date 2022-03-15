import requests from './HttpService';

const CategoryServices = {
  getAllCategory() {
    return requests.get('/category/all');
  },

  addCategory(body) {
    return requests.post('/category/add', body);
  },

  updateCategory(id, body) {
    return requests.put(`/category/${id}`, body);
  },

  deleteCategory(id, body) {
    return requests.patch(`/category/${id}`, body);
  },
};

export default CategoryServices;
