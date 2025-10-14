import React from 'react';
import { Layout, Menu, Breadcrumb } from 'antd';
import {
    DesktopOutlined,
    PieChartOutlined,
    FileOutlined,
    TeamOutlined,
    UserOutlined,
    ProfileOutlined,
    MessageOutlined,
    SettingOutlined,
    InfoCircleOutlined,
} from '@ant-design/icons';

const { Header, Content, Footer, Sider } = Layout;

const RootLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [collapsed, setCollapsed] = React.useState(false);

    return (
        <Layout style={{ minHeight: '100vh' }}>
            {/* Sidebar */}
            <Sider collapsible collapsed={collapsed} onCollapse={setCollapsed} theme='light'>
                <div className="logo" style={{ color: 'black', height: 32, margin: 16, textAlign: 'center' }}>
                    <h2>Humanity</h2>
                </div>
                <Menu theme="light" defaultSelectedKeys={['1']} mode="inline">
                    <Menu.Item key="1" icon={<PieChartOutlined />}>
                        Dashboard
                    </Menu.Item>
                    <Menu.Item key="2" icon={<UserOutlined />}>
                        Talabalar
                    </Menu.Item>
                    <Menu.Item key="6" icon={<ProfileOutlined />}>
                        Kurslar
                    </Menu.Item>
                    <Menu.Item key="3" icon={<DesktopOutlined />}>
                        Meeting
                    </Menu.Item>
                    <Menu.Item key="7" icon={<MessageOutlined />}>
                        Xabarnoma
                    </Menu.Item>
                    <Menu.SubMenu key="sub1" icon={<UserOutlined />} title="Foydalanuvchilar">
                        <Menu.Item key="4">Ro'yxat</Menu.Item>
                        <Menu.Item key="5">TEST</Menu.Item>
                    </Menu.SubMenu>
                    <Menu.SubMenu key="8" icon={<SettingOutlined />} title="Sozlamalar">
                        <Menu.Item key="9" icon={<InfoCircleOutlined />}>Profil</Menu.Item>
                        <Menu.Item key="10">TEST</Menu.Item>
                    </Menu.SubMenu>
                </Menu>
            </Sider>

            {/* Main Layout */}
            <Layout className="site-layout">
                <Header style={{ padding: 0, background: '#fff', display: "flex", height: 72 }}>
                    <h2 style={{ marginLeft: 16 }}>Welcome to Humanity</h2>
                    <div style={{ display: "flex", alignItems: "center", marginLeft: "auto", marginRight: 20 }}>
                        <img
                            src="https://cdn-icons-png.flaticon.com/512/3135/3135715.png"
                            alt="User Avatar"
                            style={{
                                width: 42,
                                height: 42,
                                borderRadius: '50%',
                                cursor: 'pointer',
                                border: '1px solid #d9d9d9',
                            }}
                        />
                    </div>
                </Header>
                <Content style={{ margin: '16px' }}>
                    <Breadcrumb style={{ margin: '16px 0' }}>
                        <Breadcrumb.Item>Home</Breadcrumb.Item>
                        <Breadcrumb.Item>Dashboard</Breadcrumb.Item>
                        <Breadcrumb.Item>Analytics</Breadcrumb.Item>
                    </Breadcrumb>
                    <div style={{ padding: 24, minHeight: 360, background: '#fff' }}>
                        {children}
                    </div>
                </Content>
                <Footer style={{ textAlign: 'center' }}>
                    Humanity ©2025 Created by You
                </Footer>
            </Layout>
        </Layout>
    );
};

export default RootLayout;
