import axios from "axios";

const API_BASE_URL = "https://localhost:7149/api/students";

// ---------- TYPES ----------
export interface CreateStudentCommand {
  id?: string;
  fullName: {
    firstName: string;
    lastName: string;
    middleName?: string;
  };
  phoneNumber: string;
  passportData: {
    series: string;
    number: string;
  };
  email: string;
}

export interface StudentResponse {
  id: string;
  fullName: string;
  uniqueCode: string;
  phoneNumber: string;
  passportData: {
    series: string;
    number: string;
  };
  email: string;
}

export interface GetAllStudentsQuery {
  search?: string;
  page?: number;
  pageSize?: number;
}

// ---------- SERVICE ----------
export const studentService = {
  /**
   * Yangi student qo‘shish
   */
  async createStudent(command: CreateStudentCommand): Promise<void> {
    await axios.post(API_BASE_URL, command);
  },

  /**
   * Barcha studentlarni olish
   */
  async getAllStudents(query?: GetAllStudentsQuery): Promise<StudentResponse[]> {
    const response = await axios.get(API_BASE_URL, { params: query });
    return response.data;
  },
};

export {};
