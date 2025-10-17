import React, { useEffect, useState } from "react";
import { Button, message, Spin } from "antd";
import StudentTable from "../components/StudentTable";
import StudentFilter from "../components/StudentFilter";
import { StudentResponse, studentService } from "../services/studentService";

const StudentListHeader: React.FC = () => {
  const [students, setStudents] = useState<StudentResponse[]>([]);
  const [loading, setLoading] = useState(false);

  const fetchStudents = async (searchText?: string) => {
    try {
      setLoading(true);
      const data = await studentService.getAllStudents();
      const filtered = searchText
        ? data.filter((s) =>
            `${s.fullName}`
              .toLowerCase()
              .includes(searchText.toLowerCase()) 
              || s.uniqueCode.toLowerCase().includes(searchText.toLowerCase())
          )
        : data;
      setStudents(filtered);
    } catch (error) {
      message.error("O'quvchilarni yuklashda xatolik yuz berdi!");
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const onSearch = (value: string) => {
    fetchStudents(value);
  };

  useEffect(() => {
    fetchStudents();
  }, []);

  return (
    <div className="flex flex-col gap-4 bg-gray-100">
      {/* Header qismi */}
      <div className="bg-white h-auto p-4 flex items-center justify-between rounded-md">
        <h1 className="text-gray-500 font-bold text-2xl">
          O'quvchilar ro'yxati
        </h1>

        <Button type="primary" className="font-bold px-4 py-2">
          O'quvchi qo‘shish
        </Button>
      </div>

      {/* Jadval qismi */}
      <div className="min-h-48 bg-white rounded-md p-4 flex flex-col gap-4">
        <StudentFilter onSearch={onSearch} />
        <hr className="my-4 border-gray-200" />

        {loading ? (
          <div className="flex justify-center py-10">
            <Spin size="large" />
          </div>
        ) : (
          <>
            <p className="self-end font-bold text-[16px]">
              O'quvchilar soni: {students.length}
            </p>
            <StudentTable data={students} />
          </>
        )}
      </div>
    </div>
  );
};

export default StudentListHeader;
