export default [
  {
    path: '/user',
    layout: false,
    routes: [
      {
        path: '/user',
        routes: [
          {
            name: 'login',
            path: '/user/login',
            component: './Login',
          },
          {
            name: 'register',
            path: '/user/register',
            component: './Register',
          },
        ],
      },
    ],
  },
  {
    path: '/',
    name: 'home',
    //access: 'loggedIn',
    //authority: ['User'],
    routes: [
      { path: '/', redirect: '/albums' },
      {
        path: '/albums',
        name: 'albums',
        icon: 'folder',
        component: './Album/List',
      },
      {
        path: '/photos',
        name: 'photos',
        icon: 'picture',
        component: './Photo/List',
      },
      {
        path: '/album',
        hideInMenu: true,
        routes: [
          {
            path: '/album/add',
            name: 'add-album',
            component: './Album',
          },
          {
            path: '/album/edit',
            name: 'edit-album',
            component: './Album',
          },
        ],
      },
      {
        path: '/photo',
        hideInMenu: true,
        routes: [
          {
            path: '/photo/add',
            name: 'add-photo',
            component: './Photo',
          },
          {
            path: '/photo/edit',
            name: 'edit-photo',
            component: './Photo',
          },
        ],
      },
    ],
  },
  {
    component: './404',
  },
];
