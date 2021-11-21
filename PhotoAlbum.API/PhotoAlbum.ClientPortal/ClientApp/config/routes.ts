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
    authority: ['User'],
    component: './Album/List',
  },
  {
    path: '/photos',
    hideInMenu: false,
    name: 'photos',
    icon: 'picture',
    access: 'loggedIn',
    authority: ['User'],
    component: './Photo/List',
  },
  {
    path: '/album',
    hideInMenu: true,
    access: 'loggedIn',
    authority: ['User'],
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
    access: 'loggedIn',
    authority: ['User'],
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
  {
    path: '/',
    redirect: '/albums',
  },
  {
    component: './404',
  },
];
