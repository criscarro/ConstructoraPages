import AuthGuard from "../Components/authGuard";
import Sidebar from "../Components/sidebar";


export default function AppLayout({ children }) {
  return (
    <AuthGuard>
      <div style={{ display: "flex" }}>
        <Sidebar />
        <div style={{ flex: 1, padding: "2rem" }}>{children}</div>
      </div>
    </AuthGuard>
  );
}