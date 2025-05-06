import React from "react";
import { Badge } from "@/components/ui/badge";
import { cn } from "@/lib/utils";

interface StatusBadgeProps {
  active: boolean;
  activeText?: string;
  inactiveText?: string;
  className?: string;
}

export function StatusBadge({
  active,
  activeText = "Active",
  inactiveText = "Inactive",
  className,
}: StatusBadgeProps) {
  return (
    <Badge
      variant={active ? "default" : "outline"}
      className={cn(
        active ? "bg-green-100 text-green-800 hover:bg-green-100" : "text-muted-foreground",
        className
      )}
    >
      {active ? activeText : inactiveText}
    </Badge>
  );
}