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
    path: '/albums',
    name: 'albums',
    icon: 'folder',
    component: './Album/List',
    access: 'loggedIn',
    authority: ['User'],
  },
  {
    path: '/photos',
    name: 'photos',
    icon: 'picture',
    component: './Photo/List',
    access: 'loggedIn',
    authority: ['User'],
  },
  {
    path: '/',
    access: 'loggedIn',
    authority: ['User'],
    routes: [
      { path: '/', redirect: '/albums' },
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
