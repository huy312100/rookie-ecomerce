import requests from './HttpService';

const BrandServices = {
  getAllBrand() {
    return requests.get('/brand/all');
  }
};

export default BrandServices;