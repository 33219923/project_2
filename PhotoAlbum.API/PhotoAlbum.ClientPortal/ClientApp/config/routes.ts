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
          {
            component: './404',
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
    routes: [
      {
        component: './404',
      },
    ],
  },
  {
    path: '/photos',
    name: 'photos',
    icon: 'picture',
    component: './Photo/List',
    access: 'loggedIn',
    authority: ['User'],
    routes: [
      {
        component: './404',
      },
    ],
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
          {
            path: '/album/view',
            name: 'view-album',
            component: './Album/View',
          },
          {
            component: './404',
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
          {
            path: '/photo/view',
            name: 'view-photo',
            component: './Photo/View',
          },
          {
            component: './404',
          },
        ],
      },
    ],
  },
  {
    component: './404',
  },
];
