import Head from 'next/head';
import React, { ReactNode } from 'react';

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
      <script async src='https://platform.twitter.com/widgets.js' charSet='utf-8'></script>
    </Head>
    <div className='container mx-auto'>{children}</div>
  </div>
);

export default Layout;
