import Image from 'next/image';
import Link from 'next/link';

const Logo = () => {
  return (
    <Link href='/'>
      <a className='flex items-center gap-2'>
        <Image
          src='/images/fletchling_logo.png'
          alt='Logo'
          width={35}
          height={35}
          className='bg-red-500 rounded-lg'
        />

        <div className='text-xl text-red-500'>Fletchling</div>
      </a>
    </Link>
  );
};

export default Logo;
