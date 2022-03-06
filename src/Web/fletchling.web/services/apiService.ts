import axios, { AxiosRequestConfig } from 'axios';

import { CreateJwtRequest, SearchTwitterUserRequest } from '@/interfaces/ApiRequests';
import { CreateJwtResponse, SearchTwitterUserResponse } from '@/interfaces/ApiResponses';
import TwitterUser from '@/interfaces/TwitterUser';
import RouteConstants from '@/lib/routeConstants';

const baseConfig: AxiosRequestConfig = {
  baseURL: process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL,
  timeout: 30000 // 30 seconds
};

const customAxios = axios.create(baseConfig);

const apiService = {
  createJwt: async function (createJwtRequest: CreateJwtRequest): Promise<string | null> {
    try {
      const { data } = await customAxios.post<CreateJwtResponse>(
        RouteConstants.AUTH.CREATE_JWT,
        createJwtRequest
      );

      return data.jwt;
    } catch (error) {
      // TODO: implement a generic error handler
      if (axios.isAxiosError(error)) {
        console.log(error.response?.status);
        console.log(error.response?.data);
      }
      return null;
    }
  },

  searchTwitterUser: async function (
    searchTwitterUserRequest: SearchTwitterUserRequest
  ): Promise<TwitterUser[]> {
    try {
      const { data } = await customAxios.get<SearchTwitterUserResponse>(
        RouteConstants.TWITTER.SEARCH_USER,
        {
          params: { username: searchTwitterUserRequest.username },
          headers: { Authorization: `Bearer ${searchTwitterUserRequest.jwt}` }
        }
      );

      return data.users;
    } catch (error) {
      // TODO: implement a generic error handler
      if (axios.isAxiosError(error)) {
        // console.log(error.response?.status);
        // console.log(error.response?.data);
      }
      throw error;
    }
  }
};

export default apiService;
