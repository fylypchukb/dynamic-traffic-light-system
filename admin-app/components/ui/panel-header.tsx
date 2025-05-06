import React from "react";
import { Button } from "@/components/ui/button";
import { PlusIcon } from "lucide-react";
import Link from "next/link";

interface PanelHeaderProps {
  title: string;
  description?: string;
  createHref?: string;
  createText?: string;
}

export function PanelHeader({
  title,
  description,
  createHref,
  createText = "Create New",
}: PanelHeaderProps) {
  return (
    <div className="flex items-start justify-between mb-8">
      <div>
        <h1 className="text-3xl font-bold tracking-tight">{title}</h1>
        {description && (
          <p className="text-muted-foreground mt-1">{description}</p>
        )}
      </div>
      {createHref && (
        <Button asChild>
          <Link href={createHref}>
            <PlusIcon className="h-4 w-4 mr-2" />
            {createText}
          </Link>
        </Button>
      )}
    </div>
  );
}