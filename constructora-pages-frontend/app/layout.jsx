import { Geist, Geist_Mono, Space_Grotesk } from "next/font/google";
import "./globals.css";
import AuthGuard from "./Components/authGuard";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

const spaceGrotesk = Space_Grotesk({
  variable: "--font-space-grotesk",
  subsets: ["latin"],
});

export const metadata = {
  title: "Constructora Pages",
  description: "Sistema de gestión de obras",
};

export default function RootLayout({ children }) {
  return (
    <html
      lang="es"
      className={`${geistSans.variable} ${geistMono.variable} ${spaceGrotesk.variable} h-full antialiased`}
    >
      <body className="min-h-full flex flex-col">
        <AuthGuard>{children}</AuthGuard>
      </body>

    </html>
  );
}
