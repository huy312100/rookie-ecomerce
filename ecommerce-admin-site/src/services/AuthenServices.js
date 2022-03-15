import requests from './HttpService';

const AuthenServices = {
  loginAdmin(body) {
    return requests.post(`/user/admin/login`, body);
  },
};

export default AuthenServices;
