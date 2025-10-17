import { Button, DatePicker, Input, Select } from "antd"
import Search from "antd/es/input/Search"
import { Option } from "antd/es/mentions"

interface StudentFilterProps {
    onSearch: (value: string) => void
}

const StudentFilter: React.FC<StudentFilterProps> = ({ onSearch }) => {
    return (
        <div className="flex flex-col gap-4">
            <div className="flex flex-row gap-3 items-center">
                <Search
                    className="flex-grow"
                    placeholder="Qidirish..."
                    onSearch={onSearch}
                    allowClear
                    style={{ width: 500 }}
                />
                <Button variant="link" color="blue" className="underline font-bold">
                    Filtrlarni tozalash
                </Button>
                <Button color="danger" variant="filled" className="font-bold px-5 py-3 rounded-ld">
                    Deactivlashtirish
                </Button>
                <Button color="blue" variant="filled" className="font-bold px-5 py-3">
                    Activlashtirish
                </Button>
                <Button type="primary" className="font-bold px-5 py-3">
                    Biriktirish
                </Button>
                <Button variant="filled" color="green" className="px-5 py-3 border-gray-300 font-bold">
                    Import Excel
                </Button>
            </div>
            <div className="flex flex-row gap-3 items-center">
                <div className="flex flex-row gap-3">
                    <Select placeholder="Status" style={{ width: 150 }} allowClear>
                        <Option value="Active">Active</Option>
                        <Option value="Inactive">Inactive</Option>
                    </Select>
                    <DatePicker
                        placeholder="Kiritilgan sana"
                        style={{ width: 220 }}
                        allowClear
                    />
                    <Input placeholder="Kursi" />
                </div>
            </div>
        </div>
    )
}

export default StudentFilter;