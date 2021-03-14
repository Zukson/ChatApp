export const environment = {
  production: true,
  user:{
    postAvatar:'http://localhost:8080/api/v1/user/postAvatar',
    getAvatar:'http://localhost:8080/api/v1/user/getAvatar',
    getInfo:'http://localhost:8080/api/v1/user/getInfo',
    getThumbnail:'http://localhost:8080/api/v1/user/getThumbnail'
  },
  
  identity:{
    register:'http://localhost:8080/api/v1/identity/register',
    login:'http://localhost:8080/api/v1/identity/login',
    refreshToken:'http://localhost:8080/api/v1/identity/refresh'

  },

  chat:
  {
    sendMessage:'http://localhost:8080/api/v1/chat/sendMessage',
    chathub:'http://localhost:8080/chathub',
    createChat:'http://localhost:8080/api/v1/chat/createChat',
    connectChats:'http://localhost:8080/api/v1/chat/connectChats',
    getChatRooms:'http://localhost:8080/api/v1/chat/getChats',
    getChatUsers:'http://localhost:8080/api/v1/chat/getChatUsers',
    getMessages:'http://localhost:8080/api/v1/chat/getMessages'
  }
};
