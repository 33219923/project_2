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
    access: 'loggedIn',
    component: './Album/List',
  },
  {
    path: '/album/add',
    hideInMenu: true,
    name: 'add-album',
    access: 'loggedIn',
    component: './Album',
  },
  {
    path: '/album/:id',
    hideInMenu: true,
    name: 'edit-album',
    access: 'loggedIn',
    component: './Album',
  },
  {
    path: '/photo/list',
    name: 'photos',
    icon: 'picture',
    access: 'loggedIn',
    component: './Photo/List',
  },
  {
    path: '/photo/add',
    hideInMenu: true,
    name: 'add-photo',
    access: 'loggedIn',
    component: './Photo',
  },
  {
    path: '/photo/:id',
    hideInMenu: true,
    name: 'edit-photo',
    access: 'loggedIn',
    component: './Photo',
  },
  {
    path: '/',
    redirect: '/albums',
    access: 'loggedIn',
  },
  {
    component: './404',
  },
];
