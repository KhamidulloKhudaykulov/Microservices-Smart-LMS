import React from "react";
import { Table, Tag } from "antd";
import type { ColumnsType } from "antd/es/table";
import { StudentResponse } from "../services/studentService";

interface Props {
  data: StudentResponse[];
  loading?: boolean;
}

const StudentTable: React.FC<Props> = ({ data, loading }) => {
  const columns: ColumnsType<StudentResponse> = [
    {
      title: "O'quvchi ID",
      dataIndex: "uniqueCode",
      key: "uniqueCode",
      width: 110,
      render: (id: string) => (
        <span style={{ fontWeight: 600, color: "#1677ff", cursor: "pointer" }}>
          {id}
        </span>
      ),
    },
    {
      title: "FIO",
      key: "fullName",
      dataIndex: "fullName",
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
    },
    {
      title: "Telefon raqami",
      dataIndex: "phoneNumber",
      key: "phoneNumber",
    },
    {
      title: "Statusi",
      key: "status",
      render: () => (
        <Tag color="green">Active</Tag> // backendda status bo‘lmasa, shunchaki placeholder
      ),
    },
  ];

  const rowSelection = {
    onChange: (selectedRowKeys: React.Key[], selectedRows: StudentResponse[]) => {
      console.log("Selected Row Keys:", selectedRowKeys);
      console.log("Selected Rows:", selectedRows);
    },
  };

  return (
    <Table
      rowKey="id"
      rowSelection={rowSelection}
      columns={columns}
      dataSource={data}
      loading={loading}
      pagination={{ pageSize: 10 }}
    />
  );
};

export default StudentTable;
