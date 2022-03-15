import requests from "./HttpService";

const UserServices = {
  getAllUsers(body) {
    return requests.get(`/user/all`, body);
  },

};

export default UserServices;
