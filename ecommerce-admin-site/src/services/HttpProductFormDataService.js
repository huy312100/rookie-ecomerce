import axios from 'axios';
import Cookies from 'js-cookie';


const instance = axios.create({
  baseURL: `https://localhost:7273/api`,
  timeout: 500000,
  headers: {
    'Content-Type': 'multipart/form-data',
  },
});

instance.interceptors.request.use(function (config) {
  let adminInfo;
  if (Cookies.get('adminInfo')) {
    adminInfo = JSON.parse(Cookies.get('adminInfo'));
  }
  return {
    ...config,
    headers: {
      authorization: adminInfo ? `Bearer ${adminInfo}` : null,
    },
  };
});

const responseBody = (response) => response.data;


const requestsFormData = {
  post: (url, data) => {
    var body = new FormData();

    body.append('Name', data.Name);
    body.append('Price', data.Price); 
    body.append('Description', data.Description);
    body.append('Image', data.Image);
    body.append('CategoryId', data.CategoryId);
    body.append('BrandId', data.BrandId);

    return instance.post(url, body).then(responseBody);
  },

  put: (url, data) =>{
    var body = new FormData();

    body.append('Id', data.Id);
    body.append('Name', data.Name);
    body.append('Price', data.Price); 
    body.append('Description', data.Description);
    body.append('Image', data.Image);
    body.append('CategoryId', data.CategoryId);
    body.append('BrandId', data.BrandId);

    return instance.put(url, body).then(responseBody);
  },
    
};

export default requestsFormData;