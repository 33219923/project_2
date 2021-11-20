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
            name: 'registrer',
            path: '/user/register',
            component: './Register',
          },
        ],
      },
      {
        component: './404',
      },
    ],
  },
  {
    path: '/albums',
    name: 'albums',
    icon: 'folder',
    authority: 'User',
    component: './Album/List',
  },
  {
    path: '/album/add',
    hideInMenu: true,
    name: 'add-album',
    authority: 'User',
    component: './Album',
  },
  {
    path: '/album/:id',
    hideInMenu: true,
    name: 'edit-album',
    authority: 'User',
    component: './Album',
  },
  {
    path: '/photo/list',
    name: 'photos',
    icon: 'picture',
    authority: 'User',
    component: './Photo/List',
  },
  {
    path: '/photo/add',
    hideInMenu: true,
    name: 'add-photo',
    authority: 'User',
    component: './Photo',
  },
  {
    path: '/photo/:id',
    hideInMenu: true,
    name: 'edit-photo',
    authority: 'User',
    component: './Photo',
  },
  {
    path: '/',
    redirect: '/albums',
  },
  {
    component: './404',
  },
];
