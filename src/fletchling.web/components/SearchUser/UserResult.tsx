import User from '@/interfaces/User';

interface Props {
  user: User;
}

const UserResult = ({ user }: Props) => {
  return (
    <div className='bg-gray-300 p-3 border-2'>
      <div>{user.displayName}</div>
      <div>@{user.username}</div>
      <button className='bg-blue-500 rounded p-2'>Add</button>
    </div>
  );
};

export default UserResult;
