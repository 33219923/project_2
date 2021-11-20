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
      {
        component: './404',
      },
    ],
  },
  {
    hideInMenu: false,
    path: '/albums',
    name: 'albums',
    icon: 'folder',
    authority: ['User'],
    component: './Album/List',
    routes: [
      {
        component: './404',
      },
    ],
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
      {
        component: './404',
      },
    ],
  },
  {
    path: '/photos',
    name: 'photos',
    icon: 'picture',
    access: 'loggedIn',
    authority: ['User'],
    component: './Photo/List',
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
      {
        component: './404',
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
