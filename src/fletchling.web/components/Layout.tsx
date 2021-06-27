import Head from 'next/head';
import React, { ReactNode } from 'react';

import NavBar from '@/components/NavBar';

type Props = {
  children?: ReactNode;
  title?: string;
};

const Layout = ({ children, title = 'This is the default title' }: Props) => (
  <div>
    <Head>
      <title>{title}</title>
      <meta charSet='utf-8' />
      <meta name='viewport' content='initial-scale=1.0, width=device-width' />
    </Head>
    <NavBar />
    <div className='px-3'>{children}</div>
  </div>
);

export default Layout;
