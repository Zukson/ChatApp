// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
 
  user:{
    postAvatar:'http://localhost:52304/api/v1/user/postAvatar',
    getAvatar:'http://localhost:52304/api/v1/user/getAvatar',
    getInfo:'http://localhost:52304/api/v1/user/getInfo',
    getThumbnail:'http://localhost:52304/api/v1/user/getThumbnail'
  },
  
  identity:{
       register:'http://localhost:52304/api/v1/identity/register',
       login:'http://localhost:52304/api/v1/identity/login',
       refreshToken:'http://localhost:52304/api/v1/identity/refresh'

  },

  chat:
  {
    sendMessage:'http://localhost:52304/api/v1/chat/sendMessage',
    chathub:'http://localhost:52304/chathub',
    createChat:'http://localhost:52304/api/v1/chat/createChat',
    connectChats:'http://localhost:52304/api/v1/chat/connectChats',
    getChatRooms:'http://localhost:52304/api/v1/chat/getChats',
    getChatUsers:'http://localhost:52304/api/v1/chat/getChatUsers'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
