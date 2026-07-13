"use client";

import { useEffect, useState } from "react";
import { usePathname, useRouter } from "next/navigation";
import { obtenerCargo } from "../Lib/auth";

export default function AuthGuard({ children }) {
  const pathname = usePathname();
  const router = useRouter();
  const [listo, setListo] = useState(false);

  useEffect(() => {
    const cargo = obtenerCargo();

    if (!cargo && pathname !== "/Login") {
      router.push("/Login");
    } else {
      setListo(true);
    }
  }, [pathname, router]);

  if (!listo && pathname !== "/Login") {
    return null;
  }

  return children;
}