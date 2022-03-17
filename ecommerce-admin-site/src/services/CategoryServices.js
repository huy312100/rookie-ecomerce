import requests from './HttpService';

const CategoryServices = {
  getAllCategory() {
    return requests.get('/category/all');
  },

  addCategory(body) {
    return requests.post('/category', body);
  },

  // updateCategory(id, body) {
  //   return requests.put(`/category/${id}`, body);
  // },

  deleteCategory(categoryId) {
    return requests.delete(`/category/${categoryId}`);
  },
};

export default CategoryServices;
