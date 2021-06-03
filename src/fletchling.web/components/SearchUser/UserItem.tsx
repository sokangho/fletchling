import User from '@/interfaces/User';

interface Props {
  user: User;
}

const UserItem = ({ user }: Props) => {
  return (
    <div className='bg-gray-300 p-3 border-2'>
      <div>{user.id}</div>
      <div>{user.username}</div>
      <div>{user.displayName}</div>
      <button className='bg-blue-500 rounded p-2'>Add</button>
    </div>
  );
};

export default UserItem;
