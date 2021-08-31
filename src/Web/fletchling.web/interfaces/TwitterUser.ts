interface TwitterUser {
  id: bigint;
  username: string;
  displayName: string;
  verified: boolean;
  profileUrl: string;
  profileImageUrl: string;
}

export default TwitterUser;
